using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using APV.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class SensorsController
    {
        protected ILogger<Controllers.SensorsController> _loggerController;
        private Mock<ControllerContext> _mockControllerContext;

        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerController = loggerFactory.CreateLogger<Controllers.SensorsController>();
            _mockControllerContext = new Mock<ControllerContext>();
        }


        [TestMethod]
        public void SaveAndGetSensorsWorks()
        {
            Controllers.SensorsController sensorController = new Controllers.SensorsController(_loggerController,
                new MockImplementations.SensorService(new List<Sensor>()),
                new MockImplementations.MeasurementService(new List<Measurement>()));

            Dictionary<string, StringValues> formData = new Dictionary<string, StringValues>();

            formData["sensors"] = "[{\"Position\": { \"Item1\": 169, \"Item2\": 345}}]";

            FormCollection keyValuePairs = new FormCollection(formData);
            var controllerContext = new ControllerContext()
            {
                HttpContext = Mock.Of<HttpContext>(ctx => ctx.Request.Form == keyValuePairs)
            };

            sensorController.ControllerContext = controllerContext;

            string result = sensorController.Save();

            Assert.AreEqual("true", result.ToLower());

            string sensor = sensorController.Get();
            Assert.IsTrue(sensor.Contains("\"Name\":null,\"Position\":{\"Item1\":169,\"Item2\":345}}]"));

        }

        [TestMethod]
        public void SaveAndGetPlanWorks()
        {
            Controllers.SensorsController sensorController = new Controllers.SensorsController(_loggerController,
                new MockImplementations.SensorService(new List<Sensor>()),
                new MockImplementations.MeasurementService(new List<Measurement>()));

            Dictionary<string, StringValues> formData = new Dictionary<string, StringValues>();

            string plan = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\plan.jpg";
            formData["planjpg"] = System.Text.Encoding.ASCII.GetString(File.ReadAllBytes(plan));

            FormCollection keyValuePairs = new FormCollection(formData);
            var controllerContext = new ControllerContext()
            {
                HttpContext = Mock.Of<HttpContext>(ctx => ctx.Request.Form == keyValuePairs)
            };

            sensorController.ControllerContext = controllerContext;

            string result = sensorController.SavePlan();

            Assert.AreEqual("true", result.ToLower());

            Assert.AreEqual(File.ReadAllBytes(plan).Length, sensorController.GetPlan().Length);
        }
    }
}