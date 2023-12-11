using APV.Service.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class MeasurementsService
    {
        [TestMethod]
        public void AddThenGetMeasurementSuccesful()
        {
            Services.MeasurementService measurementService = 
                new Services.MeasurementService("mongodb://admin:Example@localhost:27017");
            string id = "Test" + DateTime.Now.Ticks.ToString();
            bool ret = measurementService.AddMeasurement(id, 33);
            Assert.IsTrue(ret);

            Services.Measurement? measurement = measurementService.GetMeasurement(id);
            Assert.IsNotNull(measurement);
            Assert.AreEqual(33, measurement.Value);
        }        
    }
}
