using APV.Service.Controllers;
using APV.Service.Database;
using APV.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

using var logFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IMeasurementService>( _ => new MeasurementService(
                logFactory.CreateLogger<MeasurementService>(),
                new MongoDataManager(logFactory.CreateLogger<MongoDataManager>(),
                    Environment.GetEnvironmentVariable("MEASUREMENTSDB_CONNECTIONSTRING"),
                    "Measurements",
                    "Readings")));
builder.Services.AddScoped<ISensorService>( _ => new SensorService(
                logFactory.CreateLogger<SensorService>(),
                Environment.GetEnvironmentVariable("SENSORSERVICE_CONFIGFILE"),
                Environment.GetEnvironmentVariable("SENSORSERVICE_IMAGEPATH")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
