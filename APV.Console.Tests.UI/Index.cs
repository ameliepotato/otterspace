using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace APV.Console.Tests.UI
{
    [TestFixture]
    public class Index: Page
    {
        [Test]
        public void LoadPlanPictureSuccesful()
        {
            _webDriver.Url = "http://localhost:37070/";
            List<IWebElement> links = _webDriver.FindElements(By.Id("planImg")).ToList();
            Assert.That(links.Count, Is.EqualTo(1));
            IWebElement element = links[0];
            Assert.That(element.TagName, Is.EqualTo("img"));
        }

        [Test]
        public void LoadReadingsSuccesful()
        {
            _webDriver.Url = "http://localhost:37070/";
            List<IWebElement> links = _webDriver.FindElements(By.ClassName("overlay-text")).ToList();
            Assert.That(links.Count, Is.EqualTo(3));
        }
    }
}