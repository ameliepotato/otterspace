namespace APV.Service.Services
{
    public class Sensor
    {
        public Sensor(string id, Tuple<int, int> position, string? name)
        {
            Id = id;
            Position = position;
            Name = name;
        }

        public string Id { get; set; }

        public string? Name { get; set; }

        public Tuple<int, int> Position { get; set; }

    }
}
