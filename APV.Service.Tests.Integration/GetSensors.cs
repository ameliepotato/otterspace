using APV.Service.Tests.Integration.Tools;
using APV.TestTools;
using System.Dynamic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace APV.Service.Tests.Integration
{
    [TestClass]
    public class GetSensors
    {
        private string apiLocationGet = "http://localhost:37069/Sensors/GetSensors";
        private string apiLocationPost = "http://localhost:37069/Sensors/SaveSensors";

        [TestMethod]
        public void GetSensorsSuccess()
        {
            Dictionary<string, object> postParams = new Dictionary<string, object>();
            postParams.Add("sensors", "[]");
          
            string oldSensors = Request.Get(apiLocationGet);

            string response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            postParams["sensors"] = "[ { \"Position\": { \"Item1\": 163, \"Item2\": 154}}," +
                          "{ \"Position\": { \"Item1\": 263, \"Item2\": 354} } ]";

            response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

            response = Request.Get(apiLocationGet);

            List<JsonDocument> list = JsonSerializer.Deserialize<List<JsonDocument>>(response)??new List<JsonDocument>();

            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            var one = list.FirstOrDefault();

            Assert.IsNotNull(one);
            Assert.IsTrue(ObjectProperties.IsPropertyValueEqual(one, "Position",  new Tuple<int,int>(163, 154)));

            var two = list.LastOrDefault();

            Assert.IsNotNull(two);
            Assert.IsTrue(ObjectProperties.IsPropertyValueEqual(two, "Position", new Tuple<int, int>(263, 354)));

            postParams["sensors"] = oldSensors;
            response = Request.Post(apiLocationPost, postParams);

            Assert.AreEqual("true", response.ToLower());

        }
    }
}