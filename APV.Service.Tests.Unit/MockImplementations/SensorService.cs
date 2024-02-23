using APV.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace APV.Service.Tests.Unit.MockImplementations
{
    public class SensorService : ISensorService
    {
        private List<Sensor> _sensors;
        private byte[] _plan;

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

        public bool SaveSensors(List<Sensor> sensors)
        {
            _sensors = sensors;
            return true;
        }

        public List<Sensor> GetSensors()
        {
            return _sensors;
        }

        public byte[] GetPlan()
        {
            return _plan;
        }

        public bool SetPlan(byte[] plan)
        {
            _plan = plan;
            return true;
        }

    }
}
