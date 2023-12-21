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

        public List<Measurement>? GetMeasurements(string? sensorId = null, DateTime? from = null, DateTime? to = null, bool orderDescending = false)
        {
            List<Measurement> measurements = new List<Measurement>();
            if(string.IsNullOrEmpty(sensorId))
            {
                measurements = _measurements;
            }
            else
            {
                measurements = _measurements.Where(m => m.SensorId == sensorId).ToList();
            }
            return measurements;
        }
    }
}
