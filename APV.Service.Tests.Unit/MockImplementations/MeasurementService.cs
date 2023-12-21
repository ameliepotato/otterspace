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
    }
}
