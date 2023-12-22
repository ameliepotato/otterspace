using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Console.Tests.Unit.MockImplementations
{
    public class SensorHistoryManager : ISensorHistoryManager
    {
        private List<SensorHistoryEntryModel> _history;
        public SensorHistoryManager(List<SensorHistoryEntryModel> history)
        {
            _history = history;
        }
        public List<SensorHistoryEntryModel>? GetSensorHistory(string sensorId, DateTime from, DateTime? to)
        {
            return _history?.Where(x => x.RegisteredOn >= from && x.RegisteredOn <= to)?.ToList();
        }

        public static List<SensorHistoryEntryModel> GetFour()
        {
            return new List<SensorHistoryEntryModel>()
            {
                new SensorHistoryEntryModel()
                {
                      RegisteredOn = DateTime.Now,
                      Temperature = 22
                },
                new SensorHistoryEntryModel()
                {
                      Temperature = 20,
                      RegisteredOn = DateTime.Now.AddDays(-2)
                },
                new SensorHistoryEntryModel()
                {
                    Temperature = -3,
                    RegisteredOn = DateTime.Now.AddYears(-2)
                },
                new SensorHistoryEntryModel()
                {
                    Temperature = 10,
                    RegisteredOn = DateTime.Now.AddMonths(-2)
                }
            };
        }
    }
}
