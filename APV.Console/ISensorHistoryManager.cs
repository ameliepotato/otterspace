namespace APV.Console
{
    public interface ISensorHistoryManager
    {
        public abstract List<SensorHistoryEntryModel>? GetSensorHistory(string sensorId, DateTime from, DateTime? to = null);
    }
}
