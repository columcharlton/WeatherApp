using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Class;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class PastWeatherController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {

            //Set date for initial call
            DateTime reportDate = Convert.ToDateTime("11 /01/1985");
           
            //Calls news api
            //News(reportDate);

            
            Coordinates coordinates = new Coordinates();
            

            //Set location for initial call upon web start up 
            double Lat = 53.2707;
            double Lon = -9.0568;


            //Need reverse UnixTimeStamp here and cast it to a long
            DateTime ReverseTimeStamp = reportDate;
            long Time = ((DateTimeOffset)ReverseTimeStamp).ToUnixTimeSeconds();

            
            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/" 
                + Lat + "," + Lon + "," + Time + "?exclude = currently?units=si ,flags") as HttpWebRequest;


            string rawJSON = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                rawJSON = reader.ReadToEnd();

            }

            DailyWeatherDTO dto = JsonConvert.DeserializeObject<DailyWeatherDTO>(rawJSON);

            //Timestamp converter
            DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }

            StringBuilder sb = new StringBuilder();

            //sb.Append("<table><tr><th>Weather Description</th></tr>");
            //sb.Append("<tr><td>Flags:</td><td>"  + dto.flags.units +"</td></tr>");


            sb.Append("<div class='container'>" +
                      "<div class='row'>" +
                        "<div class='col-sm'>" +
                    "<span class='tempfont'>Wind:</span>" +
                    "<span class='val swap'>" +
                    "<span class='num swip'>" +
                    dto.currently.windSpeed +
                    "</span>" +
                    "<span class='unit swap'>m/s</span>" +
                         "</div>" +
                        "<div class='col-sm'>" +
                        "<span class='tempfont'>Humidity:</span><span class='val swap'><span class='num swip'>"
                + dto.currently.humidity + "</span><span class='unit swap'>%</span></span>" +
                        "</div>" +
                        "<div class='col-sm'> " +
                        "<span class='tempfont'>Pressure:</span><span class='val swap'><span class='num swip'>"
               + dto.currently.pressure + "</span>" +
                       "<span class='unit swap'>hPa</span>" +
                        "</div>" +
                      "</div>" +
                    "</div>");


            sb.Append("<br>");




            foreach (DailyWeatherItem item in dto.daily.data)
            {
                sb.Append("<table><tr><th>Daily Weather Description</th></tr>");
                sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item.time) + "</td></tr>");
                sb.Append("<tr><td>Summary</td><td>" + item.summary + "</td></tr>");
                sb.Append("</table>");


            }


            //StringBuilder Pressure = new StringBuilder();
            //StringBuilder Temperature = new StringBuilder();
            //StringBuilder Humitity = new StringBuilder();
            //StringBuilder Wind = new StringBuilder();
            //StringBuilder Dewpoint = new StringBuilder();
            //StringBuilder Uv = new StringBuilder();


            //List<double> PressureList = new List<double>();
            //List<DateTime> TimeList = new List<DateTime>();


            //foreach (HourlyWeatherItem item2 in dto.hourly.data)
            //{
            //    //Temperature.Append("{ x: " + item2.time + ", y: " + item2.temperature + " },");
            //    Dewpoint.Append("{ x: " + item2.time + ", y: " + item2.dewPoint + " },");
            //    Humitity.Append("{ x: " + item2.time + ", y: " + item2.humidity + " },");
            //    Wind.Append("{ x: " + item2.time + ", y: " + item2.windSpeed + " },");
            //    //Pressure.Append("{ x: " + item2.time + ", y: " + item2.pressure + " },");
            //    Uv.Append("{ x: " + item2.time + ", y: " + item2.uvIndex + " },");


            //    //Temperature.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.temperature + " },");
            //    //Dewpoint.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.dewPoint + " },");
            //    //Humitity.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.humidity + " },");
            //    //Wind.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.windSpeed + " },");
            //    //Pressure.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.pressure + " },");
            //    //Uv.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.uvIndex + " },");


            //    //PressureList.Add(item2.pressure);
            //    //TimeList.Add(UnixTimeStampToDateTime(item2.time));

            //}

            ////string TemperatureV = Temperature.ToString();
            //string DewpointV = Dewpoint.ToString();
            //string HumitityV = Humitity.ToString();
            ////string PressureV = Pressure.ToString();
            //string UvV = Uv.ToString();
            //string WindV = Wind.ToString();


            ////ViewBag.Temperature = TemperatureV;
            //ViewBag.Dewpoint = DewpointV;
            //ViewBag.Humitity = HumitityV;
            ////ViewBag.Pressure = PressureV;
            //ViewBag.Uv = UvV;
            //ViewBag.Wind = WindV;

            //ViewData["Temperature"] = TemperatureV;

            //ViewData["Pressure"] = PressureV;



            ////ViewBag.P = PressureList;
            ////ViewBag.T = TimeList;


            //ViewData["Pressure"] = PressureList;
            //ViewData["Pressure"] = TimeList;



            string a = sb.ToString();

            ViewBag.a = a;

            TempData["Tag"] = a.ToString();

            rawJSON = sb.ToString();
            ViewData["Message"] = "Welcome";
            //System.Diagnostics.Debug.WriteLine("hello ");

            //return View() ;    //Passing model class object
            //return PartialView();
            //return RedirectToAction("View2");
            //return View("Index");



            double UnixTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch

                double dtDateTime = unixTimeStamp * 1000;
                return dtDateTime;
            }


            List<DataPoint> Pressure = new List<DataPoint>();
            List<DataPoint> Temperature = new List<DataPoint>();
            List<DataPoint> Dewpoint = new List<DataPoint>();
            List<DataPoint> Humitity = new List<DataPoint>();
            List<DataPoint> Uv = new List<DataPoint>();
            List<DataPoint> Wind = new List<DataPoint>();


            foreach (HourlyWeatherItem item2 in dto.hourly.data)

            {


                //    dataPoints.Add(new DataPoint(item2.time, item2.pressure));
                //    //dataPoints2.Add(new DataPoint(item2.time, new double[] { item2.temperature, item2.apparentTemperature }));
                //    //dataPoints3.Add(new DataPoint(item2.time, ));

                Pressure.Add(new DataPoint(UnixTime(item2.time), item2.pressure));
                Temperature.Add(new DataPoint(UnixTime(item2.time), item2.temperature));
                Dewpoint.Add(new DataPoint(UnixTime(item2.time), item2.dewPoint));
                Humitity.Add(new DataPoint(UnixTime(item2.time), item2.humidity));
                Uv.Add(new DataPoint(UnixTime(item2.time), item2.uvIndex));
                Wind.Add(new DataPoint(UnixTime(item2.time), item2.windSpeed));

                //dataPoints.Add(new DataPoint(UnixTime(item2.time), new double[] { item2.temperature, item2.apparentTemperature }));
                //    //dataPoints.Add(new DataPoint(item2.time, new double[] { item2.temperature, item2.apparentTemperature }));



            }



            ViewBag.Pressure = JsonConvert.SerializeObject(Pressure);
            ViewBag.Temperature = JsonConvert.SerializeObject(Temperature);
            ViewBag.Dewpoint = JsonConvert.SerializeObject(Dewpoint);
            ViewBag.Humitity = JsonConvert.SerializeObject(Humitity);
            ViewBag.Uv = JsonConvert.SerializeObject(Uv);
            ViewBag.Wind = JsonConvert.SerializeObject(Wind);






            return View();

        }
        

        public ActionResult View2()
        {
            return View();

        }
        //public ActionResult Index(Coordinates co, string reportLocation, DateTime reportDate)

        [HttpPost]
        public ActionResult Index(Coordinates co, string reportLocation, DateTime reportDate)
        {

            Coordinates coordinates = new Coordinates();

            //To access stored value in Jquery datepicker model
            //DateTime reportDate = coordinates.dtmDate;

            //Calls news api
            //News(reportDate);

            List<double> Coo = new List<double>();

            Coo = GoogleReverse(reportLocation);

            //Coordinates openWeatherMap = new Coordinates();



            //DarkSkiApi Dark = new DarkSkiApi();


            double Lat = Coo[0];
            double Lon = Coo[1];
           
            
            //Need reverse UnixTimeStamp here
            DateTime foo = reportDate;
            long Time = ((DateTimeOffset)foo).ToUnixTimeSeconds();

            


            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/" + Lat + "," + Lon + "," + Time + "?exclude = currently?units=[si] ,flags") as HttpWebRequest;


            string rawJSON = "";
            //string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                rawJSON = reader.ReadToEnd();
                //apiResponse = reader.ReadToEnd();

            }

            DailyWeatherDTO dto = JsonConvert.DeserializeObject<DailyWeatherDTO>(rawJSON);

            //Timestamp converter
            DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }

            StringBuilder sb = new StringBuilder();

            //sb.Append("<table><tr><th>Weather Description</th></tr>");
            //sb.Append("<tr><td>Flags:</td><td>"  + dto.flags.units +"</td></tr>");


            sb.Append("<div class='container'>" +
                      "<div class='row'>" +
                        "<div class='col-sm'>" +
                    "<span class='tempfont'>Wind:</span>" +
                    "<span class='val swap'>" +
                    "<span class='num swip'>" +
                    dto.currently.windSpeed +
                    "</span>" +
                    "<span class='unit swap'>m/s</span>" +
                         "</div>" +
                        "<div class='col-sm'>" +
                        "<span class='tempfont'>Humidity:</span><span class='val swap'><span class='num swip'>"
                + dto.currently.humidity + "</span><span class='unit swap'>%</span></span>" +
                        "</div>" +
                        "<div class='col-sm'> " +
                        "<span class='tempfont'>Pressure:</span><span class='val swap'><span class='num swip'>"
               + dto.currently.pressure + "</span>" +
                       "<span class='unit swap'>hPa</span>" +
                        "</div>" +
                      "</div>" +
                    "</div>");


            sb.Append("<br>");



            
            foreach (DailyWeatherItem item in dto.daily.data)
            {
                sb.Append("<table><tr><th>Daily Weather Description</th></tr>");
                sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item.time) + "</td></tr>");
                sb.Append("<tr><td>Summary</td><td>" + item.summary + "</td></tr>");
                sb.Append("</table>");


            }

            
            //StringBuilder Pressure = new StringBuilder();
            //StringBuilder Temperature = new StringBuilder();
            //StringBuilder Humitity = new StringBuilder();
            //StringBuilder Wind = new StringBuilder();
            //StringBuilder Dewpoint = new StringBuilder();
            //StringBuilder Uv = new StringBuilder();


            //List<double> PressureList = new List<double>();
            //List<DateTime> TimeList = new List<DateTime>();


            foreach (HourlyWeatherItem item2 in dto.hourly.data)
            {
                //Temperature.Append("{ x: " + item2.time + ", y: " + item2.temperature + " },");
                //Dewpoint.Append("{ x: " + item2.time + ", y: " + item2.dewPoint + " },");
                //Humitity.Append("{ x: " + item2.time + ", y: " + item2.humidity + " },");
                //Wind.Append("{ x: " + item2.time + ", y: " + item2.windSpeed + " },");
                ////Pressure.Append("{ x: " + item2.time + ", y: " + item2.pressure + " },");
                //Uv.Append("{ x: " + item2.time + ", y: " + item2.uvIndex + " },");


                //Temperature.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.temperature + " },");
                //Dewpoint.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.dewPoint + " },");
                //Humitity.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.humidity + " },");
                //Wind.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.windSpeed + " },");
                //Pressure.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.pressure + " },");
                //Uv.Append("{ x:" + UnixTimeStampToDateTime(item2.time) + ", y:" + item2.uvIndex + " },");


                //PressureList.Add(item2.pressure);
                //TimeList.Add(UnixTimeStampToDateTime(item2.time));

            }

            //string TemperatureV = Temperature.ToString();
            //string DewpointV = Dewpoint.ToString();
            //string HumitityV = Humitity.ToString();
            ////string PressureV = Pressure.ToString();
            //string UvV = Uv.ToString();
            //string WindV = Wind.ToString();


            ////ViewBag.Temperature = TemperatureV;
            //ViewBag.Dewpoint = DewpointV;
            //ViewBag.Humitity = HumitityV;
            ////ViewBag.Pressure = PressureV;
            //ViewBag.Uv = UvV;
            //ViewBag.Wind = WindV;

            //ViewData["Temperature"] = TemperatureV;

            //ViewData["Pressure"] = PressureV;



            ////ViewBag.P = PressureList;
            ////ViewBag.T = TimeList;


            //ViewData["Pressure"] = PressureList;
            //ViewData["Pressure"] = TimeList;



            string a = sb.ToString();

            ViewBag.a = a;

            TempData["Tag"] = a.ToString();

            rawJSON = sb.ToString();
            ViewData["Message"] = "Welcome";
            //System.Diagnostics.Debug.WriteLine("hello ");

            //return View() ;    //Passing model class object
            //return PartialView();
            //return RedirectToAction("View2");
            //return View("Index");



            double UnixTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch

                double dtDateTime = unixTimeStamp * 1000;
                return dtDateTime;
            }



            


            List<DataPoint> Pressure = new List<DataPoint>();
            List<DataPoint> Temperature = new List<DataPoint>();
            List<DataPoint> Dewpoint = new List<DataPoint>();
            List<DataPoint> Humitity = new List<DataPoint>();
            List<DataPoint> Uv = new List<DataPoint>();
            List<DataPoint> Wind = new List<DataPoint>();


            foreach (HourlyWeatherItem item2 in dto.hourly.data)

            {


                //    dataPoints.Add(new DataPoint(item2.time, item2.pressure));
                //    //dataPoints2.Add(new DataPoint(item2.time, new double[] { item2.temperature, item2.apparentTemperature }));
                //    //dataPoints3.Add(new DataPoint(item2.time, ));
                
                Pressure.Add(new DataPoint(UnixTime(item2.time), item2.pressure));
                Temperature.Add(new DataPoint(UnixTime(item2.time), item2.temperature));
                Dewpoint.Add(new DataPoint(UnixTime(item2.time), item2.dewPoint));
                Humitity.Add(new DataPoint(UnixTime(item2.time), item2.humidity));
                Uv.Add(new DataPoint(UnixTime(item2.time), item2.uvIndex));
                Wind.Add(new DataPoint(UnixTime(item2.time), item2.windSpeed));

                //dataPoints.Add(new DataPoint(UnixTime(item2.time), new double[] { item2.temperature, item2.apparentTemperature }));
                //    //dataPoints.Add(new DataPoint(item2.time, new double[] { item2.temperature, item2.apparentTemperature }));



            }

            

            ViewBag.Pressure = JsonConvert.SerializeObject(Pressure);
            ViewBag.Temperature = JsonConvert.SerializeObject(Temperature);
            ViewBag.Dewpoint = JsonConvert.SerializeObject(Dewpoint);
            ViewBag.Humitity = JsonConvert.SerializeObject(Humitity);
            ViewBag.Uv = JsonConvert.SerializeObject(Uv);
            ViewBag.Wind = JsonConvert.SerializeObject(Wind);

            



            return View();

        }












        public List<double> GoogleReverse(string reportName)
        {

            string address = reportName;

            //"18 + Bridgewter + court + galway + irleland"

            //string address = "galway irleland";
            //string address = "1600 + Amphitheatre + Parkway,+Mountain + View,+CA";
            HttpWebRequest apiRequest = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyCY4XX-om0CBwXnmF0XbqM2M1kiUcOsZ3Q") as HttpWebRequest;

            //string rawJSON = "";
            string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();

            }


            RootObject geo = JsonConvert.DeserializeObject<RootObject>(apiResponse);

            StringBuilder sb = new StringBuilder();

            sb.Append(geo.status);


            //created a list to take in values from my object
            List<double> c = new List<double>();
            //c.Add(12345.23);

            double elem;        //Declaring elements to store lat and long
            double elem2;

            foreach (Result item in geo.results)
            {

                elem = item.geometry.location.lat;
                elem2 = item.geometry.location.lng;

                c.Add(item.geometry.location.lat);
                c.Add(item.geometry.location.lng);

                break;

            }


            double e = c[0];
            double f = c[1];

            string a = sb.ToString();


            return (c);
        }


        //News Api method
        public int News(DateTime reportDate)

        {
            //DateTime Time = reportDate;

            string Time = "1981/11";


            News news = new News();

            HttpWebRequest apiRequest = WebRequest.Create("https://api.nytimes.com/svc/archive/v1/" + Time + ".json?api-key=ZNClJjWLl8jmdrUDf8wFD34zhiGt7sjo") as HttpWebRequest;


            //int date = 1;


            string rawJSONNews = "";
            //string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                rawJSONNews = reader.ReadToEnd();
                //apiResponse = reader.ReadToEnd();

            }

            RootObject2 rootObject = JsonConvert.DeserializeObject<RootObject2>(rawJSONNews);




            StringBuilder sb = new StringBuilder();

            //sb.Append("<table><tr><th>Weather Description</th></tr>");




            //Gets the first item in the list and breaks the loop
            foreach (Docs item in rootObject.response.docs)
            {
                sb.Append("<table><tr><th>Past weaher</th></tr>");

                sb.Append("<tr><td>Summary</td><td>" + item.headline.main + "</td></tr>");
                sb.Append("<tr><td>Time:</td><td>" + item.pub_date.Day + "</td></tr>");
                sb.Append("</table>");

                break;
            }


            //created a list to take in values from my object
            IList<News> newsList = new List<News>();

            foreach (Docs item in rootObject.response.docs)
            {

                news.Headline = item.headline.main;
                news.Date = item.pub_date.Day;
                news.Lead = item.lead_paragraph;
                news.Snippet = item.snippet;
                news.Web_url = item.web_url;

                newsList.Add(new News()
                {
                    Headline = news.Headline,
                    Date = news.Date,
                    Lead = news.Lead,
                    Snippet = news.Snippet,
                    Web_url = news.Web_url
                });

            }


            int date = 1;


            News i = newsList.FirstOrDefault(o => o.Date == date);
            //var i = newsList.FirstOrDefault(o => o.Date == date);
            if (i != null)
                i.Date = date;

            //result = i.Headline;
            ViewBag.headline = i.Headline;
            ViewBag.lead = i.Lead;
            ViewBag.snippet = i.Snippet;
            ViewBag.url = i.Web_url;


            //string a = sb.ToString();

            //ViewBag.a = a;

            return (date) ;
            //return View("Index");
        }





    }

}