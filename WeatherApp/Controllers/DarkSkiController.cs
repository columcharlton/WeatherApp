using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;   //HttpWebRequest & HttpWebResponse files, HttpWebRequest = This class provides http specific implementation of WebRequest class.  WebRequest = This abstract class makes a request to uniform resource identifier.
using System.IO;    //StreamReader class, reads the Json
using System.Text;  //String builder
using Newtonsoft.Json;  //JsonConvert class

using WeatherApp.Class;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class DarkSkiController : Controller
    {
        public ActionResult Index()
        {

            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/53.2707,-9.0568?units=si") as HttpWebRequest;

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

            foreach (DailyWeatherItem item in dto.daily.data)
            {
                sb.Append("<table><tr><th>Weather Description</th></tr>");
                sb.Append("<tr><td>Flags:</td><td>" + dto.flags.units + "</td></tr>");
                sb.Append("<tr><td>Which in English means:</td><td>" + UnixTimeStampToDateTime(item.time) + "</td></tr>");
                sb.Append("<tr><td>Time:</td><td>" + item.time + "</td></tr>");
                sb.Append("<tr><td>Time:</td><td>" + item.summary + "</td></tr>");
                sb.Append("<tr><td>Time:</td><td>" + item.icon + "</td></tr>");
                sb.Append("<tr><td>High:</td><td>" + item.temperatureMax + "</td></tr>");
                sb.Append("<tr><td>Low:</td><td>" + item.temperatureLow + "</td></tr>");
                sb.Append("<tr><td>Humidity:</td><td>" + item.humidity + "</td></tr>");
                sb.Append("<tr><td>Low:</td><td>" + item.pressure + "</td></tr>");
                sb.Append("<tr><td>Chance of Precipitation:</td><td>" + item.precipType + "</td></tr>");
                sb.Append("<tr><td>Wind Speed:</td><td>" + item.windSpeed + "</td></tr>");
                sb.Append("</table>");

                foreach (HourlyWeatherItem item2 in dto.Hourly.data)
                {
                    sb.Append("<table><tr><th>Weather Description</th></tr>");
                    sb.Append("<tr><td>Flags:</td><td>" + dto.flags.units + "</td></tr>");
                    sb.Append("<tr><td>Which in English means:</td><td>" + UnixTimeStampToDateTime(item2.time) + "</td></tr>");
                    sb.Append("<tr><td>Time:</td><td>" + item2.time + "</td></tr>");
                    sb.Append("<tr><td>Time:</td><td>" + item2.summary + "</td></tr>");
                    sb.Append("<tr><td>Time:</td><td>" + item2.icon + "</td></tr>");

                    sb.Append("<tr><td>High:</td><td>" + item2.apparentTemperature + "</td></tr>");
                    sb.Append("<tr><td>Low:</td><td>" + item2.dewPoint + "</td></tr>");
                    sb.Append("<tr><td>Low:</td><td>" + item2.pressure + "</td></tr>");
                    sb.Append("<tr><td>Humidity:</td><td>" + item2.humidity + "</td></tr>");
                    sb.Append("<tr><td>Chance of Precipitation:</td><td>" + item2.precipType + "</td></tr>");
                    sb.Append("<tr><td>Wind Speed:</td><td>" + item2.windSpeed + "</td></tr>");
                    sb.Append("</table>");

                }

            }

            string a = sb.ToString();

            ViewBag.a = a;


            //rawJSON = sb.ToString();
            //ViewData["Message"] = "Welcome";
            System.Diagnostics.Debug.WriteLine("hello ");

            return View();    //Passing model class object
            //return View();
        }

    }
}