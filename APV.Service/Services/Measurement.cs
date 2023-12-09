namespace APV.Service.Services
{
    public class Measurement
    {
        public Measurement(string sensorID, int measurement, DateTime? time = null)
        {
            SensorID = sensorID;
            Value = measurement;
            Time = time ?? DateTime.Now;
        }
        public string SensorID { get; set; }
        public int Value { get; set; }

        public DateTime? Time { get; set; }
    }
}
