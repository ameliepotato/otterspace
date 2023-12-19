using OpenQA.Selenium.DevTools.V118.Network;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace APV.Console.Tests.Integration
{
    public class Index : WebTest
    {
        [Test]
        public void ReadingsSuccesful()
        {
            var reading = new
            {
                SensorId = "Fake",
                Value = 45,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now
            };

            List<object> list = new List<object>();
            list.Add(reading);
            string data = JsonSerializer.Serialize(list);
            Task task = Task.Run(() =>
            {
                Tools.Server.StartListeningOnAndRespondWith(
                    $"http://{IPREADINGSSERVICE}:{PORTREADINGSSERVICE}/", data);
            });
            _webDriver.Url = $"http://{IPWEBSITE}:{PORTWEBSITE}";
            IWebElement myReading = _webDriver.FindElement(By.Id("Fake"));
            List<IWebElement>? allReadings = _webDriver.FindElements(By.ClassName("overlay-text"))?.ToList();

            Assert.That(allReadings, Is.Not.Null);
            Assert.That(allReadings.Count(), Is.EqualTo(1));
        }

        [Test]
        public void NoReadingsErrorMessageAppers()
        {
            string data = JsonSerializer.Serialize(new List<object>());
            Task task = Task.Run(() =>
            {
                Tools.Server.StartListeningOnAndRespondWith(
                    $"http://{IPREADINGSSERVICE}:{PORTREADINGSSERVICE}/", data);
            });
            _webDriver.Url = $"http://{IPWEBSITE}:{PORTWEBSITE}";
            IWebElement myReading = _webDriver.FindElement(By.Id("error"));
            Assert.That(myReading, Is.Not.Null);
            List<IWebElement>? allReadings = _webDriver.FindElements(By.ClassName("overlay-text"))?.ToList();
            Assert.That(allReadings, Is.Not.Null);
            Assert.That(allReadings.Count, Is.EqualTo(0));  
        }
    }
}