using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Console.Tests.Unit.MockImplementations
{
    public class ReadingsManager : IReadingsManager
    {
        private List<ReadingModel> _readings;
        public ReadingsManager(List<ReadingModel> readings) {
            _readings = readings;
        }
        public List<ReadingModel>? GetAllLatestReadings()
        {
            return _readings;
        }

        public static List<ReadingModel> GetOne()
        {
            return new List<ReadingModel>()
            {
                new ReadingModel
                {
                    Value = 10,
                    PositionX = 310,
                    PositionY = 100,
                    SensorId = "Test",
                    Time = DateTime.Now
                }
            };
        }
        public static List<ReadingModel> GetTwo()
        {
            return new List<ReadingModel>()
            {
                new ReadingModel()
                {
                    Value = 10,
                    PositionX = 310,
                    PositionY = 100,
                    SensorId = "Test1",
                    Time = DateTime.Now
                },
                new ReadingModel()
                {
                    Value = 30,
                    PositionX = 510,
                    PositionY = 700,
                    SensorId = "Test2",
                    Time = DateTime.Now.AddDays(-1)
                }
            };
        }

        public static List<ReadingModel> GetEdges()
        {
            return new List<ReadingModel>()
            {
                new ReadingModel()
                {
                    Value = Constants.MINTEMPERATURE,
                    PositionX = 310,
                    PositionY = 100,
                    SensorId = "Test1",
                    Time = DateTime.Now
                },
                new ReadingModel()
                {
                    Value = Constants.IDEALTEMPERATURE,
                    PositionX = 510,
                    PositionY = 700,
                    SensorId = "Test2",
                    Time = DateTime.Now.AddDays(-1)
                },
                new ReadingModel()
                {
                    Value = Constants.MAXTEMPERATURE,
                    PositionX = 30,
                    PositionY = 770,
                    SensorId = "Test3",
                    Time = DateTime.Now.AddDays(-2)
                },
            };
        }
    }
}
