namespace APV.Service.Services
{
    public class SensorHistoryEntry
    {
        public SensorHistoryEntry(Measurement reading)
        {
            Temperature = reading.Value;
            RegisteredOn = reading.Time;
        }
        public int Temperature { get; set; }

        public DateTime RegisteredOn { get; set; }
    }
}
