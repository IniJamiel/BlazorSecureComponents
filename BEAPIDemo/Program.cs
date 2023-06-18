using System.Reflection;
using System.Runtime.InteropServices;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureBackEndAuthorizer;
using SmtpServer;

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

var connectionString = "server=localhost;user=root;password=sa;database=SkripsiBos";
var serverVersion = new MySqlServerVersion(new Version(10, 11, 3));
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseMySql(connectionString, serverVersion, a => a.MigrationsAssembly("BEAPIDemo"));
}, ServiceLifetime.Scoped);
UserContext.options =
    new DbContextOptionsBuilder<UserContext>().UseMySql(connectionString, serverVersion, a => a.MigrationsAssembly("BEAPIDemo")).Options;

SMTPSettings.SenderEmail = "isaiahjamiel2@gmail.com";
SMTPSettings.SenderPassword = "wqcwdxdxtqqwrhxj";
SMTPSettings.Server = "smtp.gmail.com";
SMTPSettings.Port = 587;



var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();

