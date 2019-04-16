using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
        public class ImageController : Controller
        {
            [HttpGet]
            public ActionResult Add()
            {
                return View();
            }


            //Upon posting an image by the user, and setting the parameters set in the model, 
           //the model is then passed to the method.
            [HttpPost]
            public ActionResult Add(Image imageModel)
            {
            //Image imageModel = new Image();    

                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                imageModel.ImageFile.SaveAs(fileName);

            
            using (DBModelEntities db = new DBModelEntities())
                {
                    db.Images.Add(imageModel);
                    db.SaveChanges();
                }
                ModelState.Clear();
                return View();
            }

        //Action result to display an individual image
        [HttpGet]
        public ActionResult View(int id)
        {
            Image imageModel = new Image();

            //Searching the data base for the first instance with the desired id
            using (DBModelEntities db = new DBModelEntities())
            {
                imageModel = db.Images.Where(x => x.ImageId == id).FirstOrDefault();
            }

            return View(imageModel);
        }

        //Action result to display a list of images
        [HttpGet]
        public ActionResult Display()
        {
            Image imageModel = new Image();

            DBModelEntities db = new DBModelEntities();
            
            return View(db.Images.ToList()); //Returns a list of images
        }


    }
    }
