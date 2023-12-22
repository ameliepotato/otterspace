using APV.TestTools;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace APV.Service.Tests.Integration
{
    [TestClass]
    public class SubmitReading
    {
        private string apiLocationGet = "http://localhost:37069/Readings/GetAllLatest";
        private string apiLocationPost = "http://localhost:37069/Readings/SubmitReading";
        [TestMethod]
        public void SubmitReadingSuccess()
        {
            //arrange
            Dictionary<string, object> postParams = new Dictionary<string, object>();
            
            postParams.Add("sensorid", "One");
            postParams.Add("temperature", 22);

            //act
            string response = Request.Post(apiLocationPost, postParams);

            //assert
            Assert.AreEqual("true", response.ToLower());
        }

        [TestMethod]
        public void SubmitReadingFailsNoIDInPostParameters()
        {
            //act
            string response = Request.Post(apiLocationPost, new Dictionary<string, object>());

            //assert
            Assert.AreEqual("no sensor id", response);
        }

        [TestMethod]
        public void SubmitReadingFailsInvalidID()
        {
            //arrange
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("sensorid", "Four");
            postParameters.Add("temperature", 33);

            //act
            string response = Request.Post(apiLocationPost, postParameters);

            //assert
            Assert.AreEqual("invalid sensor id", response);
        }
    }
}