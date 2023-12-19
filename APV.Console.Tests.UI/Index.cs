using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using System.Xml.Linq;

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
            /*<div class="image-container">
            <img src="/plan.jpg" id="planImg" alt="floor plan image">
                <div id="One" class="overlay-text" style="left: 161px; top: 362px; background-color: #91C13D">
                    22°
                </div>
                <div id="Two" class="overlay-text" style="left: 530px; top: 360px; background-color: #C7701B">
                    34°
                </div>
                <div id="Five" class="overlay-text" style="left: 664px; top: 538px; background-color: #C7701B">
                    34°
                </div>
            </div>*/
            IWebElement? link = links.Find(x => x.GetAttribute("id") == "One");
            Assert.That(link, Is.Not.Null);
            Assert.That(link.GetAttribute("style").Contains("left: 161px; top: 362px;"));
            string color  = link.GetCssValue("background-color");
            Assert.That(color.Length, Is.AtLeast(1));

            link = links.Find(x => x.GetAttribute("id") == "Two");
            Assert.That(link, Is.Not.Null);
            Assert.That(link.GetAttribute("style").Contains("left: 530px; top: 360px"));
            color = link.GetCssValue("background-color");
            Assert.That(color.Length, Is.AtLeast(1));

            link = links.Find(x => x.GetAttribute("id") == "Five");
            Assert.That(link, Is.Not.Null);
            Assert.That(link.GetAttribute("style").Contains("left: 664px; top: 538px"));
            color = link.GetCssValue("background-color");
            Assert.That(color.Length, Is.AtLeast(1));
        }

        [Test] 
        public void DisplayAverageTemperatureWorks()
        {
            //h1
            _webDriver.Url = "http://localhost:37070/";
            List<IWebElement> webElement = _webDriver.FindElements(By.XPath("//p")).ToList();
            Assert.That(webElement.Count, Is.EqualTo(1));
            Assert.That(webElement[0].Text, Is.EqualTo("Average temperature:"));
            webElement = _webDriver.FindElements(By.XPath("//h1")).ToList();
            Assert.That(webElement.Count, Is.EqualTo(1));
            Assert.That(webElement[0].Text, Is.EqualTo("14°"));
        }
    }
}