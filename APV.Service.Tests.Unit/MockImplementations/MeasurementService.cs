using APV.Service.Services;

namespace APV.Service.Tests.Unit.MockImplementations
{
    public class MeasurementService : IMeasurementService
    {
        private List<Measurement> _measurements;

        public MeasurementService(List<Measurement>? list)
        {
            _measurements = list ?? new List<Measurement>();
        }
        public bool AddMeasurement(Measurement measurement)
        {
            _measurements.Add(measurement);
            return true;
        }

        public List<Measurement>? GetAllLatestMeasurements()
        {
            return _measurements.OrderByDescending(m => m.Time).GroupBy(m => m.SensorId)?.Select(x => x.First())?.ToList();
        }

        public List<SensorHistoryEntry>? GetSensorHistory(string sensorId, DateTime from, DateTime? to)
        {
            return _measurements.Where( x => x.SensorId == sensorId && x.Time >= from && x.Time <= to)?
                .Select( x => new SensorHistoryEntry()
                {
                    Temperature = x.Value,
                    RegisteredOn = x.Time
                })?
                .ToList();
            
        }
    }
}
