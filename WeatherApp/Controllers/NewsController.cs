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
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(DateTime reportDate)

        {
            DateTime time = reportDate;
            
            //The month and year have to be separated as per the API documentation 
            int day =   time.Day;
            int month = time.Month;
            int year = time.Year;

            //Instantiating an object of the news model class
            News news = new News();

            HttpWebRequest apiRequest = WebRequest.Create("https://api.nytimes.com/svc/archive/v1/" 
                + year + "/" + month + ".json?api-key=ZNClJjWLl8jmdrUDf8wFD34zhiGt7sjo") as HttpWebRequest;
            
            string rawJSONNews = "";

            try { 

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                rawJSONNews = reader.ReadToEnd();
                
            }

            RootObject2 rootObject = JsonConvert.DeserializeObject<RootObject2>(rawJSONNews);

            

            //created a list to take in values from my object
            IList<News> newsList = new List<News>();
            

            foreach (Docs item in rootObject.response.docs)
            {

                //Extracting required data from Api response
                news.Headline = item.headline.main;
                news.Date = item.pub_date.Day;
                news.Lead = item.lead_paragraph;
                news.Snippet = item.snippet;
                news.Web_url = item.web_url;

                
                //Adding that data in my News class through the constructor to set parameters
                newsList.Add(new News() { Headline = news.Headline, Date = news.Date, Lead = news.Lead,
                    Snippet = news.Snippet, Web_url = news.Web_url});

            }


            //Searching through the recorded results for the day, the dataset is very difficult to manipulate
            int date = day;

            //Searching for the first instance of the required date
            News i = newsList.FirstOrDefault(o => o.Date == date);
            //Checking that that date exists and data is within it
            if (i != null)
                i.Date = date;

            //Returning the required data to the view
            ViewBag.date = reportDate;
            ViewBag.headline = i.Headline;
            ViewBag.lead = i.Lead;
            ViewBag.snippet = i.Snippet;
            ViewBag.url = i.Web_url;


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Connection to API invalid: '{e}'");
            }


            return View(); //returns the model data to the view

        }


    }

}

