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
            string? result = controller.GetReadings();

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

            result = controller.GetReadings(sensorID, 1);

            Assert.IsTrue(result.Contains(expectedResult));
        }

        [TestMethod]
        public void SubmitAndGetReadingsBySensorSuccessful()
        {
            // Arrange
            string sensorIdOne = "One";
            string sensorIdTwo = "Two";
                   
            MockImplementations.SensorService ss = new MockImplementations.SensorService(new List<Services.Sensor>()
            {
                new Services.Sensor(sensorIdOne, new Tuple<int, int>(1,2), null),
                new Services.Sensor(sensorIdTwo, new Tuple<int, int>(3,4), null)

            });
            Controllers.ReadingsController controller =
                new Controllers.ReadingsController(_loggerController, ss,
                    new MockImplementations.MeasurementService(new List<Services.Measurement>()));

            // Act
            string result = controller.SubmitReading(sensorIdOne, 20);

            Assert.AreEqual("true", result.ToLower());

            result = controller.SubmitReading(sensorIdOne, 30);

            Assert.AreEqual("true", result.ToLower());

            result = controller.SubmitReading(sensorIdTwo, 40);

            Assert.AreEqual("true", result.ToLower());

            result = controller.GetReadings(sensorIdOne, 1);

            Assert.IsFalse(result.Contains(sensorIdTwo));

            Assert.IsTrue(result.Contains(sensorIdOne));

            Assert.IsTrue(result.Contains($"\"Value\":20"));

            Assert.IsTrue(result.Contains($"\"Value\":30"));

            Assert.IsFalse(result.Contains($"\"Value\":40"));
        }
    }
}