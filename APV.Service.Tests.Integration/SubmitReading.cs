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
            var apiLocation = "http://localhost:32779/Reading";
            Dictionary<string, object> postParams = new Dictionary<string, object>();
            postParams.Add("id", "Reader1");
            postParams.Add("temperature", 22);
            var expectedResult = "true";

            //act
            string response = PostData.Post(apiLocation, postParams);

            //assert
            Assert.AreEqual(expectedResult, response);
        }

        [TestMethod]
        public void SubmitReadingFailsNoIDInPostParameters()
        {
            //arrange
            var apiLocation = "http://localhost:32779/Reading";
            var expectedResult = "no id";

            //act
            string response = PostData.Post(apiLocation, new Dictionary<string, object>());

            //assert
            Assert.AreEqual(expectedResult, response);
        }

        [TestMethod]
        public void SubmitReadingFailsInvalidID()
        {
            //arrange
            var apiLocation = "http://localhost:32779/Reading";
            var expectedResult = "invalid id";
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("id", "Three");
            postParameters.Add("temperature", 33);

            //act
            string response = PostData.Post(apiLocation, postParameters);

            //assert
            Assert.AreEqual(expectedResult, response);
        }

    }
}