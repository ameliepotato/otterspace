using APV.TestTools;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace APV.Service.Tests.Integration
{
    [TestClass]
    public class SubmitReading
    {
        [TestMethod]
        public void SubmitReadingSuccess()
        {
            //arrange
            var apiLocation = "http://localhost:37069/Reading";
            Dictionary<string, object> postParams = new Dictionary<string, object>();
            
            postParams.Add("id", "One");
            postParams.Add("temperature", 22);

            //act
            string response = Request.Post(apiLocation, postParams);

            //assert
            Assert.AreEqual("true", response.ToLower());
        }

        [TestMethod]
        public void SubmitReadingFailsNoIDInPostParameters()
        {
            //arrange
            var apiLocation = "http://localhost:37069/Reading";

            //act
            string response = Request.Post(apiLocation, new Dictionary<string, object>());

            //assert
            Assert.AreEqual("no id", response);
        }

        [TestMethod]
        public void SubmitReadingFailsInvalidID()
        {
            //arrange
            var apiLocation = "http://localhost:37069/Reading";
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("id", "Four");
            postParameters.Add("temperature", 33);

            //act
            string response = Request.Post(apiLocation, postParameters);

            //assert
            Assert.AreEqual("invalid id", response);
        }

        [TestMethod]
        public void GetMeasurementSuccess()
        {
            //arrange
            var apiLocation = "http://localhost:37069/Reading";
            Dictionary<string, object> postParams = new Dictionary<string, object>();

            postParams.Add("id", "One");
            postParams.Add("temperature", 22);

            //act
            string response = Request.Post(apiLocation, postParams);

            //assert
            Assert.AreEqual("true", response.ToLower());

            apiLocation = "http://localhost:37069/Reading?id=One";

            response = Request.Get(apiLocation);
            
            Assert.AreEqual("22", response?.ToLower());
        }


        //[TestMethod]
        //public void GetMeasurementFailsNoSensorId()
        //{
        //    var apiLocation = "http://localhost:37069/Reading";

        //    var response = Request.Get(apiLocation);

        //    Assert.IsNotNull(response);
        //    Assert.AreEqual("no sensor id", response?.ToLower());
        //}

        [TestMethod]
        public void GetMeasurementFailsInvalidSensorId()
        {
            var apiLocation = "http://localhost:37069/Reading?id=Inexistent";

            var response = Request.Get(apiLocation);

            Assert.IsNotNull(response);
            Assert.AreEqual("invalid sensor id", response?.ToLower());
        }


        [TestMethod]
        public void GetMeasurementFailsNoRegisteredTemperatureYet()
        {
            var apiLocation = "http://localhost:37069/Reading?id=Three";
            var response = Request.Get(apiLocation);

            Assert.IsNotNull(response);
            Assert.AreEqual("no temperature registered yet for three", response?.ToLower());
        }

    }
}