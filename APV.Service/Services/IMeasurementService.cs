namespace APV.Service.Services
{
    public interface IMeasurementService
    {
        public abstract bool AddMeasurement(Measurement measurement);
        public abstract List<Measurement>? GetMeasurements(string? sensorId = null, 
            DateTime? from = null, DateTime? to = null, bool orderDescending = false);

    }
}
