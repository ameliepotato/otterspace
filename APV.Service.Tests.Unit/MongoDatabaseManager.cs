using APV.Service.Database;
using APV.Service.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class MongoDatabaseManager
    {
        private ILogger<MongoDatabaseManager<Measurement>> _loggerManager;

        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerManager = loggerFactory.CreateLogger<MongoDatabaseManager<Measurement>>();
            _loggerManager.LogInformation("MongoDatabaseManager unit tests setup ready");
        }

        [TestMethod]
        public void GetDataSuccesful()
        {
            Database.MongoDatabaseManager<Measurement> manager =
               new Database.MongoDatabaseManager<Measurement>(_loggerManager,
                   "mongodb://admin:Example@localhost:27017", "Tests", "Integration" + DateTime.Now.Ticks.ToString());
            
            Assert.IsNotNull(manager);
            Assert.IsTrue(manager.IsConnected());

            manager.AddData(new Services.Measurement("One", 33, DateTime.Now));

            Measurement? measurement = manager.GetData();

            Assert.IsNotNull(measurement);
            Assert.AreEqual("One", measurement.SensorId);
        }

        [TestMethod]
        public void GetDataManySuccesful()
        {
            Database.MongoDatabaseManager<Measurement> manager =
               new Database.MongoDatabaseManager<Measurement>(
                   _loggerManager, "mongodb://admin:Example@localhost:27017", "Tests", "Integration" + DateTime.Now.Ticks.ToString());

            Assert.IsNotNull(manager);
            Assert.IsTrue(manager.IsConnected());

            manager.AddManyData( new List<Measurement>()
                {   
                    new Services.Measurement("One", 33, DateTime.Now), 
                    new Services.Measurement("Two", 33, DateTime.Now)
                });

            List<Services.Measurement>? measurements = manager.GetManyData()?.ToList();

            Assert.IsNotNull(measurements);
            Assert.IsTrue(measurements.Any());
            Assert.AreEqual(2, measurements.Count());
        }


        [TestMethod]
        public void CreateIndexSuccesful()
        {
            Database.MongoDatabaseManager<Measurement> manager =
               new Database.MongoDatabaseManager<Measurement>(
                   _loggerManager, "mongodb://admin:Example@localhost:27017", "Tests", "Integration" + DateTime.Now.Ticks.ToString());

            Assert.IsNotNull(manager);
            Assert.IsTrue(manager.IsConnected());

            bool result = manager.CreateIndex();

            Assert.IsTrue(result);
        }
    }
}
