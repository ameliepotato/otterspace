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
            var apiLocation = "https://localhost:32788/Reading?id=Reader1&temperature=22";
            var expectedResult = "true";

            //act
            string response = PostData.Post(apiLocation, new Dictionary<string, object>());

            //assert
            Assert.AreEqual(expectedResult, response);
        }

        [TestMethod]
        public void SubmitReadingFails()
        {
            //arrange
            var apiLocation = "https://localhost:32788/Reading";
            var expectedResult = "The id field is required.";

            //act
            string response = PostData.Post(apiLocation, new Dictionary<string, object>());

            //assert
            Assert.IsTrue(response.Contains(expectedResult));
        }
    }
}