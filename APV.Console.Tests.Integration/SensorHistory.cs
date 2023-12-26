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
                SensorId = "Fake1",
                Value = 45,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now
            };
            list.Add(reading);

            reading = new
            {
                SensorId = "Fake2",
                Value = 22,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now.AddDays(-21)
            };
            list.Add(reading);

            reading = new
            {
                SensorId = "Fake3",
                Value = 20,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now.AddDays(-1)
            };
            list.Add(reading);

            reading = new
            {
                SensorId = "Fake4",
                Value = 21,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now.AddDays(-2)
            };
            list.Add(reading);

            string data = JsonSerializer.Serialize(list);

            Tools.Server server = new Tools.Server();
            server.AddToResponseData("GetAllLatest", data);                       
           

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

            server.AddToResponseData("GetSensorHistory?sensorId=Fake1", data);

            Task task = Task.Run(() =>
            {
                server.StartListening($"http://{IPREADINGSSERVICE}:{PORTREADINGSSERVICE}/Readings/");
            });

            _webDriver.Url = $"http://{IPWEBSITE}:{PORTWEBSITE}";
            IWebElement myReading = _webDriver.FindElement(By.Id("btnFake1"));

            Assert.That(myReading, Is.Not.Null);

            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)_webDriver;
            javascriptExecutor.ExecuteScript("arguments[0].click();", myReading);

            Thread.Sleep(3000);

            List<IWebElement>? entries = _webDriver.FindElements(By.Id("chartImgFake1")).ToList();

            Assert.That(entries, Is.Not.Null);

            Assert.That(entries.Count, Is.EqualTo(1));

            task.Wait();
        }
    }
}
