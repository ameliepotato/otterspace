using APV.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Service.Tests.Unit.MockImplementations
{
    public class SensorService : ISensorService
    {
        private List<Sensor> _sensors;

        public SensorService(List<Sensor>? list) 
        {
            _sensors = list ?? new List<Sensor>();
        }
        public Sensor? FindSensor(string id)
        {
            return _sensors.Find(x => x.Id == id); 
        }

        public int SensorCount()
        {
            return _sensors.Count;
        }
    }
}
