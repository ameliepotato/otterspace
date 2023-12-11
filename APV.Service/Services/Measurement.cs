namespace APV.Service.Services
{
    public class Measurement
    {
        public Measurement(string sensorID, int measurement, DateTime? time = null)
        {
            SensorId = sensorID;
            Value = measurement;
            Time = time ?? DateTime.Now;
        }
        public string SensorId { get; set; }
        public int Value { get; set; }

        public DateTime? Time { get; set; }

        public string _id { get; set; }
    }
}
