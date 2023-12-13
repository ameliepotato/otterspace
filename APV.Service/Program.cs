using APV.Service.Controllers;
using APV.Service.Database;
using APV.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var logFactory = new LoggerFactory();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("APVServiceLogger"));
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<ISensorService>( _ => new SensorService(
                Environment.GetEnvironmentVariable("SENSORSERVICE_CONFIGFILE")));
builder.Services.AddScoped<IDataManager<Measurement>>( _ => new MongoDatabaseManager<Measurement>(
                logFactory.CreateLogger<MongoDatabaseManager<Measurement>>(),      
                Environment.GetEnvironmentVariable("MEASUREMENTSDB_CONNECTIONSTRING")??"",
                "Measurements",
                "Readings"));

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
