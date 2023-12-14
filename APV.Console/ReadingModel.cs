namespace APV.Console
{
    public class ReadingModel
    {
        public string sensorId { get; set; }
        public string sensorName { get; set; }
        public int value { get; set; }

        public DateTime time { get; set; }

        public int positionX { get; set; }
        public int positionY { get; set; }
    }
}
