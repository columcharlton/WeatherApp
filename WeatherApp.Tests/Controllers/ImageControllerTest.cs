using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp;
using WeatherApp.Controllers;

namespace WeatherApp.Tests.Controllers
{
    [TestClass]
    public class ImageControllerTest
    {
        [TestMethod]
        public void View()
        {
            // Arrange
            ImageController controller = new ImageController();

            // Act
            ViewResult result = controller.View(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        public void Display()
        {
            // Arrange
            ImageController controller = new ImageController();

            // Act
            ViewResult result = controller.Display() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
