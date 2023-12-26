namespace APV.Service.Services
{
    public interface IMeasurementService
    {
        public abstract bool AddMeasurement(Measurement measurement);
        public abstract List<Measurement>? GetAllLatestMeasurements();

        List<SensorHistoryEntry>? GetSensorHistory(string sensorId, DateTime from, DateTime? to);

    }
}
