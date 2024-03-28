using CriptografiaAPI.Application.Core;
using CriptografiaAPI.Criptografar;
using CriptografiaAPI.Infra.Context;
using CriptografiaAPI.Infra.Core;
using CriptografiaAPI.Infra.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IApplicationContext, ApplicationContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICriptografiaService, CriptografiaService>();

builder.Services.ConfigureMediator();

builder.Services.ConfigureMainDatabase(configuration);
builder.Services.CheckConnectionDatabase();
builder.Services.RunMigrations();

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
