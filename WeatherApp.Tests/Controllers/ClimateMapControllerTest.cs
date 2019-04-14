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
    public class ClimateMapControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ClimateMapController controller = new ClimateMapController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //[HttpPost]
        //public void Index()
        //{
        //    ClimateMapController homeController = new ClimateMapController();
        //    ActionResult result = homeController.Index();
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //}

    }
}
