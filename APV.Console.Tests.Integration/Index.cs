using OpenQA.Selenium.DevTools.V118.Network;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APV.Console.Tests.Integration
{
    public class Index : WebTest
    {
        private const int PORT = 37069;
        private const string IP = "127.0.0.1";  
        
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
                Tools.Server.StartListeningOnAndRespondWith($"http://{IP}:{PORT}/Readings/", data);
            });
            _webDriver.Url = $"http://{IPWEBSITE}:{PORTWEBSITE}";
            IWebElement element = _webDriver.FindElement(By.Id("readings"));
            List<IWebElement>? rows = element.FindElements(By.XPath("(.//*)[1]"))?.ToList();
            Assert.That(rows, Is.Not.Null);
            Assert.That(rows.Count(), Is.EqualTo(1));

            element = element.FindElement(By.Id("Fake"));
            Assert.That(element, Is.Not.Null);
        }
    }
}