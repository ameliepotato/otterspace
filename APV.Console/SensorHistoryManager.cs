using System;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace APV.Console
{
    public class SensorHistoryEntryModel
    {
        public int Temperature { get; set; }

        public DateTime RegisteredOn { get; set; }
    }

    public class SensorHistoryManager : ISensorHistoryManager
    {
        private readonly ILogger _logger;
        private string _url = "";

        public SensorHistoryManager(ILogger logger, string? url = null)
        {
            _logger = logger;
            _url = url ?? _url;
            _logger.LogInformation($"ReadingsManager created with url {url}");
        }

        public List<SensorHistoryEntryModel>? GetSensorHistory(string sensorId, DateTime from, DateTime? to)
        {
            var url = $"{_url}GetSensorHistory?sensorId={sensorId}";
            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError($"No url to get history from");
                return null;
            }

            _logger.LogInformation($"Getting history from {url}");
            List<SensorHistoryEntryModel>? readings = null;
            using (var httpClient = new HttpClient())
            {
                try
                { 
                    Task<string> call = httpClient.GetStringAsync(url);
                    call.Wait();
                    if (call.IsCompletedSuccessfully)
                    {
                        readings = JsonSerializer.Deserialize<List<SensorHistoryEntryModel>>(call.Result) ??
                            new List<SensorHistoryEntryModel>();
                    }
                    else
                    {
                        _logger.LogError($"Getting history from {url} failed with error: {call.Result}");
                    }
                }
                catch (TaskCanceledException cancelled)
                {
                    _logger.LogError($"Getting history from {url} was cancelled. Error: {cancelled.Message}");
                }
                catch (Exception e)
                {
                    _logger.LogError($"Getting history from {url} failed with error {e.Message}");
                }
            }
            if(readings == null || readings.Count < 1)
            {
                readings = new List<SensorHistoryEntryModel> {
                            new SensorHistoryEntryModel(){
                                RegisteredOn = DateTime.Now,
                                Temperature = 22
                            },
                            new SensorHistoryEntryModel(){
                                Temperature = 20,
                                RegisteredOn = DateTime.Now.AddDays(-2)
                            }
                        };
            }
            _logger.LogInformation($"Returning {readings?.Count} history entries");
            return readings;
        }
    }
}
