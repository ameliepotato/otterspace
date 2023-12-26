using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using APV.Service.Services;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class ReadingsController
    {
        protected ILogger<Controllers.ReadingsController> _loggerController;


        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerController = loggerFactory.CreateLogger<Controllers.ReadingsController>();
        }


        [TestMethod]
        public void SubmitReadingSuccess()
        {
            // Arrange
            string sensorId = "Two";
            MockImplementations.SensorService ss = new MockImplementations.SensorService(new List<Services.Sensor>(
                new List<Services.Sensor>()
                    {
                        new Services.Sensor(sensorId, new Tuple<int, int>(0,0), null)
                    }));
            Controllers.ReadingsController controller = new Controllers.ReadingsController(_loggerController, ss, 
                new MockImplementations.MeasurementService(new List<Services.Measurement>()));
            // Act
            string result = controller.SubmitReading("Two", 3);

            // Assert
            Assert.AreEqual("true", result.ToLower());
        }

        [TestMethod]
        public void SubmitReadingInvalidSensor()
        {
            // Arrange
            MockImplementations.SensorService ss = new MockImplementations.SensorService(new List<Services.Sensor>());
            Controllers.ReadingsController controller = 
                new Controllers.ReadingsController(_loggerController, ss, 
                    new MockImplementations.MeasurementService(new List<Services.Measurement>()));
            // Act
            string result = controller.SubmitReading("Three", 3);

            // Assert
            Assert.AreEqual("invalid sensor id", result.ToLower());
        }

        [TestMethod]
        public void GetReadingSuccessNoReadings()
        {
            // Arrange
            MockImplementations.SensorService ss = new MockImplementations.SensorService(new List<Services.Sensor>());
            Controllers.ReadingsController controller = 
                new Controllers.ReadingsController(_loggerController, ss, 
                    new MockImplementations.MeasurementService(new List<Services.Measurement>()));
            // Act
            string? result = controller.GetAllLatest();

            // Assert
            Assert.AreEqual("no measurements registered yet", result);
        }

        [TestMethod]
        public void SubmitAndGetReadingsSuccessful()
        {
            // Arrange
            string sensorID = "One";
            int temp = 2;
            int posx = 3;
            int posy = 4;
            string expectedResult = $"\"SensorId\":\"{sensorID}\",\"PositionX\":{posx},\"PositionY\":{posy},\"Value\":{temp}";

            MockImplementations.SensorService ss = new MockImplementations.SensorService(new List<Services.Sensor>()
            {
                new Services.Sensor(sensorID, new Tuple<int, int>(posx,posy), null)
            });
            Controllers.ReadingsController controller =
                new Controllers.ReadingsController(_loggerController, ss,
                    new MockImplementations.MeasurementService(new List<Services.Measurement>()));
            
            // Act
            string result = controller.SubmitReading(sensorID, temp);

            Assert.AreEqual("true", result.ToLower());

            result = controller.GetAllLatest();

            Assert.IsTrue(result.Contains(expectedResult));
        }

        [TestMethod]
        public void GetSensorHistorySuccessful()
        {
            // Arrange
            MockImplementations.SensorService ss = new MockImplementations.SensorService(new List<Services.Sensor>()
            {
                new Services.Sensor("one", new Tuple<int, int>(0,0), null),
                new Services.Sensor("two", new Tuple<int, int>(0,0), null)
            });
            DateTime date = DateTime.UtcNow;

            Controllers.ReadingsController controller =
                new Controllers.ReadingsController(_loggerController, ss,
                    new MockImplementations.MeasurementService(new List<Services.Measurement>()
                    {
                        new Measurement("one", 22, date),
                        new Measurement("one", 45, date.AddDays(-1)),
                        new Measurement("two", 33, date)
                    }));
            string output = $"{{\"Temperature\":22,\"RegisteredOn\":";

            // Act
            string result = controller.GetSensorHistory("one", DateTime.UtcNow.AddMinutes(-10));

            Assert.IsTrue(result.Contains(output));
        }
    }
}