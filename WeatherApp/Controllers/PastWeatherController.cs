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

            try
            {

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
                sb.Append("<table><tr><th>Daily Weather Description</th></tr>");
                sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item.time) + "</td></tr>");
                sb.Append("<tr><td>Summary</td><td>" + item.summary + "</td></tr>");
                sb.Append("</table>");


            }



            //sb.Append("<div class='container'>" +
            //          "<div class='row'>" +
            //            "<div class='col-sm'>" +
            //        "<span class='tempfont'>Wind:</span>" +
            //        "<span class='val swap'>" +
            //        "<span class='num swip'>" +
            //        dto.currently.windSpeed +
            //        "</span>" +
            //        "<span class='unit swap'>m/s</span>" +
            //             "</div>" +
            //            "<div class='col-sm'>" +
            //            "<span class='tempfont'>Humidity:</span><span class='val swap'><span class='num swip'>"
            //    + dto.currently.humidity + "</span><span class='unit swap'>%</span></span>" +
            //            "</div>" +
            //            "<div class='col-sm'> " +
            //            "<span class='tempfont'>Pressure:</span><span class='val swap'><span class='num swip'>"
            //   + dto.currently.pressure + "</span>" +
            //           "<span class='unit swap'>hPa</span>" +
            //            "</div>" +
            //          "</div>" +
            //        "</div>");


            //sb.Append("<br>");





            


            string a = sb.ToString();

            ViewBag.a = a;

            TempData["Tag"] = a.ToString();

            rawJSON = sb.ToString();
            ViewData["Message"] = "Welcome";
            


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




        }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Connection to API invalid: '{e}'");
            }


            return View();

        }
        
        
        
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

            double Lat = Coo[0];
            double Lon = Coo[1];
           
            
            //Need reverse UnixTimeStamp here
            DateTime foo = reportDate;
            long Time = ((DateTimeOffset)foo).ToUnixTimeSeconds();

            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/" + Lat + "," + Lon + "," + Time + "?exclude = currently?units=[si] ,flags") as HttpWebRequest;


            string rawJSON = "";
            //string apiResponse = "";


            try {  

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

            foreach (DailyWeatherItem item in dto.daily.data)
            {
                sb.Append("<table><tr><th>Daily Weather Description</th></tr>");
                sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item.time) + "</td></tr>");
                sb.Append("<tr><td>Summary</td><td>" + item.summary + "</td></tr>");
                sb.Append("</table>");


            }


            //sb.Append("<div class='container'>" +
            //          "<div class='row'>" +
            //            "<div class='col-sm'>" +
            //        "<span class='tempfont'>Wind:</span>" +
            //        "<span class='val swap'>" +
            //        "<span class='num swip'>" +
            //        dto.currently.windSpeed +
            //        "</span>" +
            //        "<span class='unit swap'>m/s</span>" +
            //             "</div>" +
            //            "<div class='col-sm'>" +
            //            "<span class='tempfont'>Humidity:</span><span class='val swap'><span class='num swip'>"
            //    + dto.currently.humidity + "</span><span class='unit swap'>%</span></span>" +
            //            "</div>" +
            //            "<div class='col-sm'> " +
            //            "<span class='tempfont'>Pressure:</span><span class='val swap'><span class='num swip'>"
            //   + dto.currently.pressure + "</span>" +
            //           "<span class='unit swap'>hPa</span>" +
            //            "</div>" +
            //          "</div>" +
            //        "</div>");


            //sb.Append("<br>");



            
            
            string a = sb.ToString();

            ViewBag.a = a;

            TempData["Tag"] = a.ToString();

            rawJSON = sb.ToString();
            

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



            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Connection to API invalid: '{e}'");
            }

            return View();

        }












        public List<double> GoogleReverse(string reportName)
        {

            string address = reportName;

            HttpWebRequest apiRequest = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyCY4XX-om0CBwXnmF0XbqM2M1kiUcOsZ3Q") as HttpWebRequest;

            string apiResponse = "";

            try {

                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();

            }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Connection to API invalid: '{e}'");
            }


            RootObject geo = JsonConvert.DeserializeObject<RootObject>(apiResponse);

            StringBuilder sb = new StringBuilder();

            sb.Append(geo.status);

            //created a list to take in values from my object
            List<double> c = new List<double>();

            foreach (Result item in geo.results)
            {

                c.Add(item.geometry.location.lat);
                c.Add(item.geometry.location.lng);

                break;

            }


            return (c);
        }

    }

}