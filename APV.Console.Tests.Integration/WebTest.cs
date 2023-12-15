using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Console.Tests.Integration
{
    [TestFixture]
    public class WebTest
    {
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
            _webDriver.Close();
        }


    }
}
