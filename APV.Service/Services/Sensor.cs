namespace APV.Service.Services
{
    public class Sensor
    {
        public Sensor(string id, KeyValuePair<int, int> position, int? measurement, string? name)
        {
            Id = id;
            Position = position;
            Measurement = measurement;
            Name = name;
        }

        public string Id { get; set; }

        public string? Name { get; set; }

        public KeyValuePair<int, int> Position { get; set; }


        public int? Measurement { get; set; }
    }
}
