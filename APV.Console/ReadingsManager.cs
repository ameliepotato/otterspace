namespace APV.Console
{
    public class ReadingsManager : IReadingsManager
    {
        public ReadingsManager(string url) 
        {
            
        }
        public List<ReadingModel>? GetReadings()
        {
            List<ReadingModel> readings = new List<ReadingModel>();
            return readings;
        }
    }
}
