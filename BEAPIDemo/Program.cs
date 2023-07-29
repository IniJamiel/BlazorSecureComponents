using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureBackEndAuthorizer;
using SmtpServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => policy.AllowAnyHeader());
});

builder.Services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("SecureBackEndAuthorizer")));

var connectionString = JsonRead.AppSetting.GetValue<string>(MainSettings.Database);
var serverVersion = new MySqlServerVersion(new Version(10, 11, 3));
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseMySql(connectionString, serverVersion, a => a.MigrationsAssembly("BEAPIDemo"));
}, ServiceLifetime.Scoped);
UserContext.options =
    new DbContextOptionsBuilder<UserContext>().UseMySql(connectionString, serverVersion, a
        => a.MigrationsAssembly("BEAPIDemo")).Options;
SMTPSettings.SenderEmail = JsonRead.AppSetting.GetValue<string>(MainSettings.SMTPSender);
SMTPSettings.SenderPassword = JsonRead.AppSetting.GetValue<string>(MainSettings.SMTPPassword);
SMTPSettings.Server = JsonRead.AppSetting.GetValue<string>(MainSettings.SMTPServer);
SMTPSettings.Port = JsonRead.AppSetting.GetValue<int>(MainSettings.SMTPPort);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Set your token validation parameters
        ValidateIssuer = true,
        ValidIssuer = MainSettings.JWTIssuer,
        ValidateAudience = true,
        ValidAudience = MainSettings.JWTAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MainSettings.JWTSecretKey)),
        ValidateIssuerSigningKey = true
    };
});


var app = builder.Build();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();

