using APV.TestTools;

namespace APV.Service.Tests.Integration
{
    [TestClass]
    public class SubmitReading
    {
        [TestMethod]
        public void SubmitReadingSuccess()
        {
            //arrange
            var apiLocation = "http://localhost:32787/Reading";
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
        public void SubmitReadingFails()
        {
            //arrange
            var apiLocation = "http://localhost:32787/Reading";
            var expectedResult = "no id";

            //act
            string response = PostData.Post(apiLocation, new Dictionary<string, object>());

            //assert
            Assert.AreEqual(expectedResult, response);
        }

    }
}