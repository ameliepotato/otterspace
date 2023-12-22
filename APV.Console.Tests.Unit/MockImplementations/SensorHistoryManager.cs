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
        public SensorHistoryManager(List<SensorHistoryEntryModel> history) {
            _history = history;
        }
        public List<SensorHistoryEntryModel>? GetSensorHistory(string sensorId, DateTime from, DateTime? to)
        {
            return GetMany();
        }

        public static List<SensorHistoryEntryModel> GetMany()
        {
            return new List<SensorHistoryEntryModel>()
            {
                new SensorHistoryEntryModel(),
                new SensorHistoryEntryModel(),
                new SensorHistoryEntryModel()
            };
        }
    }
}
