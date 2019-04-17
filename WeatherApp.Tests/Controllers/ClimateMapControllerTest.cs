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
            Assert.IsNotNull(result); //Checks the type being returned
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }






//Check out the ViewResult class, this can show you what else you could test.What you need to do is mock your DbContext and supply it with data in the Products property(DbSet<>) as this is being called in your controller's action. You can then test

//The type being returned
//The model on the ViewResult
//The ViewName which should be empty or Index
//Sample code

//[TestMethod]
//public void Test_Index()
//        {
//            //Arrange
//            ProductsController prodController = new ProductsController(); // you should mock your DbContext and pass that in

//            // Act
//            var result = prodController.Index() as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Model); // add additional checks on the Model
//            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
//        }
    }
}
