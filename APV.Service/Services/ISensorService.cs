namespace APV.Service.Services
{
    public interface ISensorService
    {
        public abstract Sensor? FindSensor(string id);
        public abstract int SensorCount();

    }
}
