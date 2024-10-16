﻿using System;
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
            _webDriver.Url = "http://localhost:37070/";
            List<IWebElement> links = _webDriver.FindElements(By.Id("btnb1ae3e86-a94f-4368-8255-ec6e4e323e0e")).ToList();
            Assert.That(links.Count, Is.EqualTo(1));

            IWebElement entry = links[0];
            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)_webDriver;
            javascriptExecutor.ExecuteScript("arguments[0].click();", entry);

            Thread.Sleep(3000);

            List<IWebElement> entries = _webDriver.FindElements(By.ClassName("modal-open")).ToList();

            Assert.That(entries.Count, Is.EqualTo(1));

            entries = _webDriver.FindElements(By.Id("historyModalb1ae3e86-a94f-4368-8255-ec6e4e323e0e")).ToList();
            Assert.That(entries.Count == 1);

            entry = entries[0].FindElement(By.ClassName("close"));

            Assert.That(entry, Is.Not.Null);

            entries = _webDriver.FindElements(By.Id("chartImgb1ae3e86-a94f-4368-8255-ec6e4e323e0e")).ToList();

            Assert.That(entries.Count, Is.EqualTo(1));

            entry.Click();
            
            entries = _webDriver.FindElements(By.ClassName("modal-open")).ToList();

            Assert.That(entries.Count == 0);   
        }
    }
}
