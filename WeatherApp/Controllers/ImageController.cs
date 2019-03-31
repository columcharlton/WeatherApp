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

            [HttpPost]
            public ActionResult Add(Image imageModel)
            {
            Image image = new Image();    

                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                imageModel.ImageFile.SaveAs(fileName);

                //image.ImageFile = fileName;

                //imageModel.ImagePath = "~/Image/" + fileName;
                //fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                //imageModel.ImageFile.SaveAs(fileName);

                //image = imageModel;


            using (MvcImageDBEntities db = new MvcImageDBEntities())
                {
                    db.Images.Add(image);
                    db.SaveChanges();
                }
                ModelState.Clear();
                return View();
            }

            [HttpGet]
            public ActionResult View(int id)
            {
                Image imageModel = new Image();

                using (MvcImageDBEntities db = new MvcImageDBEntities())
                {
                    imageModel = db.Images.Where(x => x.ImageId == id).FirstOrDefault();
                }

                return View(imageModel);
            }
        }
    }
