using APV.Service.Tests.Integration.Tools;
using APV.TestTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APV.Service.Tests.Integration
{
    [TestClass]
    public class GetSensorHistory
    {
        private string apiLocationGet = "http://localhost:37069/Readings/GetSensorHistory";
        private string apiLocationPost = "http://localhost:37069/Readings/SubmitReading";

        [TestMethod]
        public void BySensorWorks()
        {
            Dictionary<string, object> postParams = new Dictionary<string, object>();

            DateTime start = DateTime.UtcNow.AddSeconds(-1);

            postParams.Add("sensorid", "Six");
            postParams.Add("temperature", 22);

            string response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            postParams["temperature"] = 34;

            response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            DateTime end = DateTime.UtcNow.AddSeconds(1);

            response = Request.Get($"{apiLocationGet}?sensorId=Six&from={start}&to={end}");

            List<JsonDocument> list = JsonSerializer.Deserialize<List<JsonDocument>>(response) ?? new List<JsonDocument>();

            Assert.IsNotNull(list);

            Assert.IsTrue(list.Any());

            Assert.AreEqual(2, list.Count());

            JsonDocument latest = list.First();

            Assert.IsNotNull(latest);

            Assert.AreEqual(34, ObjectProperties.GetPropertyValue<int>(latest, "Temperature"));

            DateTime? registered = ObjectProperties.GetPropertyValue<DateTime>(latest, "RegisteredOn");
            
            Assert.IsNotNull(registered);

            Assert.IsTrue( registered < end );

            Assert.IsTrue(registered > start);


        }
    }
}
