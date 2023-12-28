using System.Text.Json;
using WireMock.RequestBuilders;

namespace APV.Console.Tests.Integration
{
    public class Index : WebTest
    {
        [Test]
        public void ReadingsSuccesful()
        {
            var reading = new {
                SensorId = "Fake",
                Value = 45,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now
            };

            List<object> list = new List<object>();
            list.Add(reading);
            string data = JsonSerializer.Serialize(list);

            _mockServer.Given(Request.Create().UsingGet().WithPath("/Readings/GetAllLatest"))
                .RespondWith(WireMock.ResponseBuilders.Response.Create().WithBody(data));

            _webDriver.Url = WEBSITEURL;
            IWebElement myReading = _webDriver.FindElement(By.Id("containerSensorFake"));
            List<IWebElement>? allReadings = _webDriver.FindElements(By.ClassName("overlay-text"))?.ToList();

            Assert.That(allReadings, Is.Not.Null);
            Assert.That(allReadings.Count(), Is.EqualTo(1));
        }

        [Test]
        public void NoReadingsErrorMessageAppers()
        {
            string data = JsonSerializer.Serialize(new List<object>());

            _mockServer.Given(Request.Create().UsingGet().WithPath("/Readings/GetAllLatest"))
               .RespondWith(WireMock.ResponseBuilders.Response.Create().WithBody(data));

            _webDriver.Url = WEBSITEURL;
            IWebElement myReading = _webDriver.FindElement(By.Id("error"));
            Assert.That(myReading, Is.Not.Null);
            List<IWebElement>? allReadings = _webDriver.FindElements(By.ClassName("overlay-text"))?.ToList();
            Assert.That(allReadings, Is.Not.Null);
            Assert.That(allReadings.Count, Is.EqualTo(0));  
        }
    }
}