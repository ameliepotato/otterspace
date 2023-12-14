namespace APV.Service.Services
{
    public class Reading
    {
        public string SensorId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Value { get; set; }

        public DateTime Time { get; set; }
    }
}
