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
        private string apiLocationGet = "http://localhost:37069/Readings/GetAllLatest";
        private string apiLocationPost = "http://localhost:37069/Readings/SubmitReading";
        [TestMethod]
        public void GetAllLatestMeasurementsSuccess()
        {
            Dictionary<string, object> postParams = new Dictionary<string, object>();

            string sensorOneid = "b1ae3e86-a94f-4368-8255-ec6e4e323e0e";
            postParams.Add("sensorid", sensorOneid);
            postParams.Add("temperature", 22);

            string response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            string sensorTwoid = "42fbdefa-ea35-425c-a235-4d078335419f";
            postParams["sensorid"] = sensorTwoid;
            postParams["temperature"] = 33;

            response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            response = Request.Get(apiLocationGet);

            List<JsonDocument> list = JsonSerializer.Deserialize<List<JsonDocument>>(response)??new List<JsonDocument>();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());

            var one = list.Where(x => ObjectProperties.IsPropertyValueEqual(x, "SensorId", sensorOneid)).FirstOrDefault();

            Assert.IsNotNull(one);
            Assert.IsTrue(ObjectProperties.IsPropertyValueEqual(one, "Value", 22));

            var two = list.Where(x => ObjectProperties.IsPropertyValueEqual(x, "SensorId", sensorTwoid)).FirstOrDefault();

            Assert.IsNotNull(two);
            Assert.IsTrue(ObjectProperties.IsPropertyValueEqual(two, "Value", 33));

        }

        [TestMethod]
        public void GetOneLatestMeasurementSuccess()
        {
            Dictionary<string, object> postParams = new Dictionary<string, object>();

            string sensorid = "42fbdefa-ea35-425c-a235-4d078335419f";
            postParams.Add("sensorid", sensorid);
            postParams.Add("temperature", 22);

            string response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            postParams["temperature"] = 30;

            response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            response = Request.Get(apiLocationGet);

            List<JsonDocument> list = JsonSerializer.Deserialize<List<JsonDocument>>(response) ?? new List<JsonDocument>();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());

            Assert.AreEqual(1, list.Where(x =>
                string.Compare(ObjectProperties.GetPropertyValue<string>(x, "SensorId"), sensorid, false) == 0).Count());

            JsonDocument latest = list.Where(x =>
                string.Compare(ObjectProperties.GetPropertyValue<string>(x, "SensorId"), sensorid, false) == 0).First();

            Assert.IsNotNull(latest);

            Assert.AreEqual(30, ObjectProperties.GetPropertyValue<int>(latest, "Value"));
        }
    }
}