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
        public ActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Index(Coordinates co, string reportLocation, DateTime reportDate)

        {

            List<double> Coo = new List<double>();

            Coo = GoogleReverse(reportLocation);

            //Coordinates openWeatherMap = new Coordinates();


            Coordinates coordinates = new Coordinates();

            //DarkSkiApi Dark = new DarkSkiApi();


            double Lat = Coo[0];
            double Lon = Coo[1];
            //int time = reportDate;

            //int time = 610200000;




            //Need reverse UnixTimeStamp here
            //Timestamp converter
            //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            DateTime foo = reportDate;
            long Time = ((DateTimeOffset)foo).ToUnixTimeSeconds();


            //long Lat = co.Latitude;
            //long Lon = co.Longitude;

            //double Lat = 53.2707;
            //double Lon = -9.0568;



            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/" + Lat + "," + Lon + "," + Time + "?exclude = currently?units=si") as HttpWebRequest;


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
                //sb.Append("<tr><td>Flags:</td><td>" + dto.flags.units + "</td></tr>");
                sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item.time) + "</td></tr>");
                //sb.Append("<tr><td>Time:</td><td>" + item.time + "</td></tr>");
                sb.Append("<tr><td>Summary</td><td>" + item.summary + "</td></tr>");
                //sb.Append("<tr><td>Time:</td><td>" + item.icon + "</td></tr>");
                //sb.Append("<tr><td>High:</td><td>" + item.temperatureMax + "</td></tr>");
                //sb.Append("<tr><td>Low:</td><td>" + item.temperatureLow + "</td></tr>");
                //sb.Append("<tr><td>Humidity:</td><td>" + item.humidity + "</td></tr>");
                //sb.Append("<tr><td>Low:</td><td>" + item.pressure + "</td></tr>");
                //sb.Append("<tr><td>Chance of Precipitation:</td><td>" + item.precipType + "</td></tr>");
                //sb.Append("<tr><td>Wind Speed:</td><td>" + item.windSpeed + "</td></tr>");
                sb.Append("</table>");


            }



            //foreach (HourlyWeatherItem item2 in dto.hourly.data)

            //{
            //    sb.Append("<table><tr><th>Hourly Weather Description</th></tr>");
            //    //sb.Append("<tr><td>Flags:</td><td>" + dto.flags.units + "</td></tr>");
            //    sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item2.time) + "</td></tr>");
            //    //sb.Append("<tr><td>Time:</td><td>" + item2.time + "</td></tr>");
            //    sb.Append("<tr><td>Summary:</td><td>" + item2.summary + "</td></tr>");
            //    //sb.Append("<tr><td>Time:</td><td>" + item2.icon + "</td></tr>");

            //    sb.Append("<tr><td>Apparent Temperature:</td><td>" + item2.apparentTemperature + "</td></tr>");
            //    //sb.Append("<tr><td>Low:</td><td>" + item2.dewPoint + "</td></tr>");
            //    //sb.Append("<tr><td>Low:</td><td>" + item2.pressure + "</td></tr>");
            //    //sb.Append("<tr><td>Humidity:</td><td>" + item2.humidity + "</td></tr>");
            //    //sb.Append("<tr><td>Chance of Precipitation:</td><td>" + item2.precipType + "</td></tr>");
            //    //sb.Append("<tr><td>Wind Speed:</td><td>" + item2.windSpeed + "</td></tr>");
            //    sb.Append("</table>");

            //}

            coordinates.rawJSON = sb.ToString();


            string a = sb.ToString();

            ViewBag.a = a;

            TempData["Tag"] = a.ToString();

            rawJSON = sb.ToString();
            ViewData["Message"] = "Welcome";
            //System.Diagnostics.Debug.WriteLine("hello ");

            //return View() ;    //Passing model class object
            return View(coordinates);
            //return View("Index");
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
    }
}