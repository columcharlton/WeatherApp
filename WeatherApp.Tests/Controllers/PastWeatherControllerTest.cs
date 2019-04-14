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
    public class PastWeatherControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            PastWeatherController controller = new PastWeatherController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
