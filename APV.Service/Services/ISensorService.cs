namespace APV.Service.Services
{
    public interface ISensorService
    {
        public abstract Sensor? FindSensor(string id);
        public abstract int SensorCount();
        
        public abstract bool SaveSensors(List<Sensor> sensors);

        public abstract List<Sensor> GetSensors();

        public abstract byte[] GetPlan();

        public abstract bool SetPlan(byte[] plan);

    }
}
