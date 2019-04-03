using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        //public ActionResult Index()
        //{
        //    return View();
        //}


        //[HttpPost]
        public ActionResult Index()//DateTime reportDate)

        {
            //DateTime Time = reportDate;

            News news = new News();

            HttpWebRequest apiRequest = WebRequest.Create("https://api.nytimes.com/svc/archive/v1/1981/11.json?api-key=ZNClJjWLl8jmdrUDf8wFD34zhiGt7sjo") as HttpWebRequest;



            string rawJSONNews = "";
            //string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                rawJSONNews = reader.ReadToEnd();
                //apiResponse = reader.ReadToEnd();

            }

            RootObject2 rootObject = JsonConvert.DeserializeObject<RootObject2>(news.rawJSONNews);




            StringBuilder sb = new StringBuilder();

            //sb.Append("<table><tr><th>Weather Description</th></tr>");
            

            

           
                foreach (Docs item in rootObject.response.docs  )
                {
                    sb.Append("<table><tr><th>Past weaher</th></tr>");

                sb.Append("<tr><td>Summary</td><td>" + item.headline.main + "</td></tr>");
                sb.Append("<tr><td>Time:</td><td>" + item.pub_date.Day + "</td></tr>");
                sb.Append("</table>");

                break;
                }


                //string headline;
                //int date;

            //created a list to take in values from my object
            List<News> c = new List<News>();
            

            foreach (Docs item in rootObject.response.docs)
            {

                news.Headline = item.headline.main;
                news.Date = item.pub_date.Day;
                //c.Add(headline, date);

                break;

            }


            //double e = c[0];
            //double f = c[1];

            //string a = sb.ToString();




            string a = sb.ToString();

            ViewBag.a = a;

            //TempData["Tag"] = a.ToString();

            //rawJSON = sb.ToString();
            //ViewData["Message"] = "Welcome";
            ////System.Diagnostics.Debug.WriteLine("hello ");

            //return View() ;    //Passing model class object
            return View();
            //return View("Index");
        }

    }
}