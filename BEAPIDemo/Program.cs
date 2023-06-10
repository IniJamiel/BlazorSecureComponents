using System.Reflection;
using CommonModelsLib;
using CommonModelsLib.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("SecureBackEndAuthorizer")));

var connectionString = "server=localhost;user=root;password=sa;database=SkripsiBos";
var serverVersion = new MySqlServerVersion(new Version(10, 11, 3));
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseMySql(connectionString, serverVersion, a => a.MigrationsAssembly("BEAPIDemo"));
}, ServiceLifetime.Scoped);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
