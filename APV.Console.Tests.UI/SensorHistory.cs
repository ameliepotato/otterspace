using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace APV.Console.Tests.UI
{
    [TestFixture]
    public class SensorHistory : Page
    {

        [Test]
        public void DialogShowsOnCLickAndClosesWithX()
        {
            int timeout = 20000;//20 seconds
            _webDriver.Url = "http://localhost:37070/";
            List<IWebElement> links = _webDriver.FindElements(By.Id("btnOne")).ToList();
            Assert.That(links.Count, Is.EqualTo(1));

            IWebElement entry = links[0];
            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)_webDriver;
            javascriptExecutor.ExecuteScript("arguments[0].click();", entry);

            Thread.Sleep(3000);

            List<IWebElement> entries = _webDriver.FindElements(By.ClassName("modal-open")).ToList();

            Assert.That(entries.Count, Is.EqualTo(1));

            entries = _webDriver.FindElements(By.Id("historyModalOne")).ToList();
            Assert.That(entries.Count == 1);

            entry = entries[0].FindElement(By.ClassName("close"));

            Assert.That(entry, Is.Not.Null);

            entry.Click();
            
            entries = _webDriver.FindElements(By.ClassName("modal-open")).ToList();

            Assert.That(entries.Count == 0);
        }


        [Test]
        public void DialogClosesOnTimeout()
        {
            int timeout = 30000;//milliseconds
            _webDriver.Url = "http://localhost:37070/";
            List<IWebElement> links = _webDriver.FindElements(By.ClassName("overlay-text")).ToList();
            Assert.That(links.Count, Is.EqualTo(3));
            
            IWebElement myReading = links[0];
            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)_webDriver;
            javascriptExecutor.ExecuteScript("arguments[0].click();", myReading);

            Thread.Sleep(3000);

            List<IWebElement> entries = _webDriver.FindElements(By.ClassName("modal-open")).ToList();

            Assert.That(entries.Count == 1);

            Thread.Sleep(timeout);

            entries = _webDriver.FindElements(By.ClassName("modal-open")).ToList();

            Assert.That(entries.Count, Is.EqualTo(0));

        }
    }
}
