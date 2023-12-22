using System.Diagnostics;
using System.Text.Json;

namespace APV.Console.Tests.Integration
{
    public class SensorHistory : WebTest
    {

        [Test]
        public void GetSensorHistorySuccesful()
        {
            List<object> list = new List<object>();
            var reading = new
            {
                SensorId = "Fake",
                Value = 45,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now
            };
            list.Add(reading);

            reading = new
            {
                SensorId = "Fake",
                Value = 22,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now.AddDays(-21)
            };
            list.Add(reading);

            reading = new
            {
                SensorId = "Fake",
                Value = 20,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now.AddDays(-1)
            };
            list.Add(reading);

            reading = new
            {
                SensorId = "Fake",
                Value = 21,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now.AddDays(-2)
            };
            list.Add(reading);

            string data = JsonSerializer.Serialize(list);
            
            Tools.Server.AddToResponseData("GetAllLatest", data);                       
           

            list.Clear();
            list.Add(new { 
                    Temperature = 33,
                    RegisteredOn = DateTime.UtcNow});

            list.Add(new
            {
                Temperature = 34,
                RegisteredOn = DateTime.UtcNow.AddDays(-1)
            });

            list.Add(new
            {
                Temperature = 23,
                RegisteredOn = DateTime.UtcNow.AddDays(-2)
            });

            data = JsonSerializer.Serialize(list);

            Tools.Server.AddToResponseData("GetSensorHistory?sensorId=Fake", data);

            Task task = Task.Run(() =>
            {
                Tools.Server.StartListening($"http://{IPREADINGSSERVICE}:{PORTREADINGSSERVICE}/Readings/");
            });

            _webDriver.Url = $"http://{IPWEBSITE}:{PORTWEBSITE}";
            IWebElement myReading = _webDriver.FindElement(By.Id("btnFake"));

            Assert.That(myReading, Is.Not.Null);

            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)_webDriver;
            javascriptExecutor.ExecuteScript("arguments[0].click();", myReading);

            Thread.Sleep(3000);

            List<IWebElement> entries = _webDriver.FindElements(By.XPath("//li"))
                .Where(x => GetParent(x).GetAttribute("id") == "bodyFake").ToList();

            Assert.That(3 == entries.Count);

        }
    }
}
