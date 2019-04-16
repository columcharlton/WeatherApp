using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using System.Web.UI.WebControls;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(SimpleLogin.Models.UserModel user)
        {
            if (ModelState.IsValid) //Checks if the view is valid
            {
                if (IsValid(user.Email, user.Password)) // & IsValid2(user.Email))
                {
                    bool ans = IsValid2(user.Email);

                    if (ans == true)
                    {   //True for admin :)

                        FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);   //Needs security namespace
                        return RedirectToAction("Admin", "Admin");   //And then redirect to the main page

                    }

                    else       ////False for others 

                        FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);   //Needs security namespace //Setting this cookie was to true was an issue had to add a method to do so in the Global.asax file, Application_PostAuthenticateRequest()

                    return RedirectToAction("Index", "ClimateMap");   //And then redirect to the main page

                }

                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect");
                }

            }


            return View(user);  //Send back a user
        }

        [HttpGet]
        public ActionResult Registration()  //Will display the registration view first
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(SimpleLogin.Models.UserModel user) //When create user is clicked, a user is sent in
        {
            if (ModelState.IsValid) //Checks if the view is valid
            {
                using (var db  = new DBModelEntities())    //Create a new instance of our main db context, check EF
                {
                    var crypto = new SimpleCrypto.PBKDF2(); //Create a new instance of our crypo

                    var encroPass = crypto.Compute(user.Password); //Create an encription of the passord

                    var sysUser = db.SystemUsers.Create(); //Create a new system user

                    sysUser.Email = user.Email;
                    sysUser.Password = encroPass;
                    sysUser.PasswordSalt = crypto.Salt;
                    sysUser.UserId = Guid.NewGuid();    //And store them all

                    db.SystemUsers.Add(sysUser);    //Add the entity back into the context 
                    db.SaveChanges();

                    //return RedirectToAction("Index", "Home");   //And then redirect to the main page
                    return RedirectToAction("Index", "ClimateMap");   //And then redirect to the main page

                }
            }
            else
            {
                ModelState.AddModelError("", "Login Data is incorrect");
            }

            return View(user);  //Send back a user
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "ClimateMap");   //And then redirect to the main page

        }




        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2(); //This is the type of encription we are using

            bool isValid = false;

            using (var db = new DBModelEntities())
            {
                var user = db.SystemUsers.FirstOrDefault(u => u.Email == email);

                    if (user != null)
                    {
                        if(user.Password == crypto.Compute(password, user.PasswordSalt))
                        {
                        isValid = true;

                        }
                    }
            }
            return isValid;
        }

        private bool IsValid2(string email) //Checks if you are Admin or not
        {
            bool isValid = false;

            using (var db = new DBModelEntities())
            {

                var user = db.SystemUsers.FirstOrDefault(u => u.Email == email);


                if (user != null)
                {
                    if (user.Admin == true)
                    {
                        isValid = true;
                    }

                }
            }
            return isValid;
        }

    }
}