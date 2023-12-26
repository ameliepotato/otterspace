using MongoDB.Bson.Serialization.Attributes;

namespace APV.Service.Services
{
    public class Measurement
    {
        public Measurement(string sensorID, int measurement, DateTime? time = null)
        {
            SensorId = sensorID;
            Value = measurement;
            Time = time ?? DateTime.UtcNow;
            _id = Guid.NewGuid().ToString();
        }
        public string SensorId { get; set; }
        public int Value { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]

        public DateTime Time { get; set; }

        public string _id { get; set; }
    }
}
