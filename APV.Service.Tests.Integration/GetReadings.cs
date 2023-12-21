using APV.Service.Tests.Integration.Tools;
using APV.TestTools;
using System.Dynamic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace APV.Service.Tests.Integration
{
    [TestClass]
    public class GetReadings
    {
        [TestMethod]
        public void GetAllLatestMeasurementsSuccess()
        {
            var apiLocation = "http://localhost:37069/Readings";
            Dictionary<string, object> postParams = new Dictionary<string, object>();

            postParams.Add("sensorid", "One");
            postParams.Add("temperature", 22);

            string response = Request.Post(apiLocation, postParams);

            Assert.AreEqual("true", response.ToLower());

            postParams["sensorid"] = "Two";
            postParams["temperature"] = 34;

            response = Request.Post(apiLocation, postParams);

            Assert.AreEqual("true", response.ToLower());

            apiLocation += "?GetAllLatest";

            response = Request.Get(apiLocation);

            List<JsonDocument> list = JsonSerializer.Deserialize<List<JsonDocument>>(response)??new List<JsonDocument>();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());

            var one = list.Where(x => ObjectProperties.IsPropertyValueEqual(x, "SensorId", "One")).FirstOrDefault();

            Assert.IsNotNull(one);
            Assert.IsTrue(ObjectProperties.IsPropertyValueEqual(one, "Value", 22));

            var two = list.Where(x => ObjectProperties.IsPropertyValueEqual(x, "SensorId", "Two")).FirstOrDefault();

            Assert.IsNotNull(two);
            Assert.IsTrue(ObjectProperties.IsPropertyValueEqual(two, "Value", 34));

        }

        [TestMethod]
        public void GetOneLatestMeasurementSuccess()
        {
            var apiLocation = "http://localhost:37069/Readings";
            Dictionary<string, object> postParams = new Dictionary<string, object>();

            postParams.Add("sensorid", "Five");
            postParams.Add("temperature", 22);

            string response = Request.Post(apiLocation, postParams);

            Assert.AreEqual("true", response.ToLower());

            postParams["temperature"] = 34;

            response = Request.Post(apiLocation, postParams);

            Assert.AreEqual("true", response.ToLower());

            apiLocation += "?GetAllLatest";

            response = Request.Get(apiLocation);

            List<JsonDocument> list = JsonSerializer.Deserialize<List<JsonDocument>>(response) ?? new List<JsonDocument>();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());

            Assert.AreEqual(1, list.Where(x =>
                string.Compare(ObjectProperties.GetPropertyValue<string>(x, "SensorId"), "Five", false) == 0).Count());

            JsonDocument latest = list.Where(x =>
                string.Compare(ObjectProperties.GetPropertyValue<string>(x, "SensorId"), "Five", false) == 0).First();

            Assert.IsNotNull(latest);

            Assert.AreEqual(34, ObjectProperties.GetPropertyValue<int>(latest, "Value"));
        }
    }
}