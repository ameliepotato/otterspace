using OpenQA.Selenium.Chrome;

namespace APV.Console.Tests.Integration
{
    [TestFixture]
    public class WebTest
    {
        protected const string IPREADINGSSERVICE = "localhost";
        protected const int PORTREADINGSSERVICE = 37010;
        protected const int PORTWEBSITE = 26080;
        protected const string IPWEBSITE = "localhost";
        protected IWebDriver _webDriver { get; set; }
        [SetUp]
        public void Setup()
        {
            string driverPath = Environment.GetEnvironmentVariable("CHROMEDRIVERPATH_UITESTS") ??
                Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Drivers";
            _webDriver = new ChromeDriver(driverPath);
        }

        [TearDown]
        public void CloseBrowser()
        {
            _webDriver.Quit();
        }

        public static IWebElement GetParent(IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }
    }
}
