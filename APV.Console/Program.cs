using APV.Console;
using APV.Console.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

using var logFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

builder.Services.AddLogging();

builder.Services.AddScoped<IReadingsManager>(_ =>
    new ReadingsManager(logFactory.CreateLogger<ReadingsManager>(),
        Environment.GetEnvironmentVariable("APVCONSOLE_READINGMANAGER_URL_CONSOLEINTEGRATIONTESTS") ??
            Environment.GetEnvironmentVariable("APVCONSOLE_READINGMANAGER_URL") ??
                ""));

builder.Services.AddScoped<ISensorHistoryManager>(_ =>
    new SensorHistoryManager(logFactory.CreateLogger<SensorHistoryManager>(),
        Environment.GetEnvironmentVariable("APVCONSOLE_READINGMANAGER_URL_CONSOLEINTEGRATIONTESTS") ??
            Environment.GetEnvironmentVariable("APVCONSOLE_READINGMANAGER_URL") ??
                ""));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
