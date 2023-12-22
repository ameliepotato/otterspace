using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Console.Tests.Unit
{
    public class SensorHistoryModel
    {
        ILogger<Pages.SensorHistoryModel> _logger;
        [SetUp]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Pages.SensorHistoryModel>();
        }

        [Test]
        public void OnGetIsSuccesful()
        {
            MockImplementations.SensorHistoryManager historyManager =
                new MockImplementations.SensorHistoryManager(MockImplementations.SensorHistoryManager.GetFour());
            Pages.SensorHistoryModel page = new Pages.SensorHistoryModel(_logger, historyManager);
            page.OnGet("");
            Assert.That(page._entries.Count, Is.EqualTo(1));
        }
    }
}
