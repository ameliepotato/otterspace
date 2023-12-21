using APV.Service.Services;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Service.Tests.Unit.MockImplementations
{
    public class MeasurementService : IMeasurementService
    {
        private List<Measurement> _measurements;

        public MeasurementService(List<Measurement>? list)
        {
            _measurements = list ?? new List<Measurement>();
        }
        public bool AddMeasurement(Measurement measurement)
        {
            _measurements.Add(measurement);
            return true;
        }

        public List<Measurement>? GetMeasurements(string? sensorId = null, DateTime? from = null, DateTime? to = null, bool orderDescending = false)
        {
            return _measurements;
        }
    }
}
