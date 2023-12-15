using System;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace APV.Console
{
    public class ReadingsManager : IReadingsManager
    {
        private readonly ILogger<ReadingsManager> _logger;
        static readonly HttpClient _httpClient = new HttpClient();

        public ReadingsManager(ILogger<ReadingsManager> logger)
        {
            _logger = logger;
            _logger.LogInformation("ReadingsManager created");
        }

        public List<ReadingModel>? GetReadings()
        {
            string? url = Environment.GetEnvironmentVariable("APVCONSOLE_READINGMANAGER_URL");                ;

            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError($"No url to get readings from");
                return null;
            }

            _logger.LogInformation($"Getting readings from {url}");
            List<ReadingModel>? readings = null;
            try
            {
                Task<string> call = _httpClient.GetStringAsync(url);
                call.Wait();
                if (call.IsCompletedSuccessfully)
                {
                    readings = JsonSerializer.Deserialize<List<ReadingModel>>(call.Result) ??
                        new List<ReadingModel>();
                }
                else
                {
                    _logger.LogError($"Getting readings from {url} failed with error: {call.Result}");
                }
            }
            catch(TaskCanceledException cancelled)
            {
                _logger.LogError($"Getting readings from {url} was cancelled. Error: {cancelled.Message}");
            }
            catch(Exception e)
            {
                _logger.LogError($"Getting readings from {url} failed with error {e.Message}");
            }
            _logger.LogInformation($"Returning {readings?.Count} readings");
            return readings;
        }
    }
}
