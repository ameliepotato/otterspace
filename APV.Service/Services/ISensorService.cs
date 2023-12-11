namespace APV.Service.Services
{
    public interface ISensorService
    {
        public abstract bool IsSensorRegistered(string id);
        public abstract int SensorCount();

    }
}
