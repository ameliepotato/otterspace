using APV.TestTools;
using System.Runtime.InteropServices;

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
            string response = PostData.Post(apiLocation, postParams);

            //assert
            Assert.AreEqual("true", response.ToLower());
        }

        [TestMethod]
        public void SubmitReadingFailsNoIDInPostParameters()
        {
            //arrange
            var apiLocation = "http://localhost:37069/Reading";

            //act
            string response = PostData.Post(apiLocation, new Dictionary<string, object>());

            //assert
            Assert.AreEqual("no id", response);
        }

        [TestMethod]
        public void SubmitReadingFailsInvalidID()
        {
            //arrange
            var apiLocation = "http://localhost:37069/Reading";
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("id", "Three");
            postParameters.Add("temperature", 33);

            //act
            string response = PostData.Post(apiLocation, postParameters);

            //assert
            Assert.AreEqual("invalid id", response);
        }

    }
}