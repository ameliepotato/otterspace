namespace APV.Console
{
    public class ReadingModel
    {
        public string SensorId { get; set; }
        public string SensorName { get; set; }
        public int Value { get; set; }

        public DateTime Time { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public string ColorCode { get; set; }
    }
}
