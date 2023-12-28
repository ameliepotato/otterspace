using NUnit.Framework.Constraints;
using OpenQA.Selenium.Chrome;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace APV.Console.Tests.Integration
{
    [TestFixture]
    public class WebTest
    {
        protected const string WEBSITEURL =     "http://localhost:26080/";
        protected const string MOCKSERVERURL =  "http://localhost:37010/";

        protected IWebDriver _webDriver { get; set; }

        protected WireMockServer _mockServer { get; set; }
        
        
        [SetUp]
        public void SetUp()
        {
            string driverPath = Environment.GetEnvironmentVariable("CHROMEDRIVERPATH_UITESTS") ??
                Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Drivers";
            _webDriver = new ChromeDriver(driverPath);
            _mockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Logger = new WireMockConsoleLogger(),
                Urls = new string[] { MOCKSERVERURL }
            });
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
            _mockServer.Stop();
        }

        public static IWebElement GetParent(IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }
    }
}
