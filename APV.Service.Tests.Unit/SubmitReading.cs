using Microsoft.VisualStudio.TestTools.UnitTesting;
using APV.Service.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class SubmitReading
    {
        [TestMethod]
        public void SubmitReadingSuccess()
        {
            ReadingController controller = new ReadingController(null);
            controller.ControllerContext = new ControllerContext();
            
            // Act
            string result = controller.Submit("doi", 3);

            Assert.AreEqual("true", result);
        }
    }
}