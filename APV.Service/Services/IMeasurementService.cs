namespace APV.Service.Services
{
    public interface IMeasurementService
    {
        public abstract bool AddMeasurement(string sensorID, int temp, DateTime? time = null);
        public abstract Measurement? GetMeasurement(string sensorId);

    }
}
