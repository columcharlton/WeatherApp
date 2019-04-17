using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;   
//HttpWebRequest & HttpWebResponse files, HttpWebRequest = This class provides http specific implementation of WebRequest class.  
//WebRequest = This abstract class makes a request to uniform resource identifier.
using System.IO;    //StreamReader class, reads the Json
using System.Text;  //String builder
using Newtonsoft.Json;  //JsonConvert class

//Below two namespaces to access and inherit the properties and roles are given below.
using WeatherApp.Class;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class ClimateMapController : Controller
    {

        //public ActionResult Index()
        //{

        //    return View();
        //}


        [HttpGet]
        public ActionResult Index()
        {
      
            double Lat = 53.2707;
            double Lon = -9.0568;

            
            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/" 
                + Lat + "," + Lon + "?units=si") as HttpWebRequest;

            string rawJSON = "";


            try
            {
            
                
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                rawJSON = reader.ReadToEnd();

            }

            DailyWeatherDTO dto = JsonConvert.DeserializeObject<DailyWeatherDTO>(rawJSON);


           

            //Timestamp converter method
            DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                DayOfWeek day = dtDateTime.DayOfWeek;

                return dtDateTime;
            }

            //returns a string format for use in view
            DayOfWeek Day(DateTime dateTime)
            {

                //string date = dateTime.ToString("MM:dd:yyyy");
                DayOfWeek day = dateTime.DayOfWeek;
                
                return day;
            }


            //returns a string format for use in view
            String date(DateTime dateTime)
            {

                string D = dateTime.ToString("MM:dd:yyyy");
                string DD = dateTime.ToString("yyyy, MM ,dd");
                //string DJ = dateTime.ToString("yyyy,dd ,MM");
                //DayOfWeek day = dateTime.DayOfWeek;

                return DD;
            }


            //returns a string format for use in view
            String dateHours(DateTime dateTime)
            {

                string D = dateTime.ToString("MM:dd:yyyy");
                string value = dateTime.ToString("HHmm");
                string DM = dateTime.ToString("yyyy, MM ,dd ,HHmm, ss");
                string DD = dateTime.ToString("yyyy, MM ,dd ,HH, mm, ss");
                //string DJ = dateTime.ToString("yyyy,dd ,MM");
                //DayOfWeek day = dateTime.DayOfWeek;



                return DD;
            }


            
            //Returning data to view
            ViewBag.CWindspeed = dto.currently.windSpeed;
            ViewBag.CWindbearing = dto.currently.windBearing;
            ViewBag.CHumidity = dto.currently.humidity * 100;
            ViewBag.CDewpoint = dto.currently.dewPoint;
            ViewBag.CUvindex = dto.currently.uvIndex;
            ViewBag.CVisibility = dto.currently.visibility;
            ViewBag.CPressure = dto.currently.pressure;

            ViewBag.Cicon = dto.currently.icon;
            ViewBag.Ctemperature = dto.currently.temperature;

            ViewBag.CSummary = dto.currently.summary;



            //Creating lists to add parameters needed in the view
            List<DateTime> HourlyTime = new List<DateTime>();
            List<string> HourlySummary = new List<string>();
            List<string> HourlyIcon = new List<string>();

            
            foreach (HourlyWeatherItem item2 in dto.hourly.data)

            {
                //Obtaining hourly data for use in view
                HourlyTime.Add(UnixTimeStampToDateTime(item2.time));
                HourlySummary.Add(item2.summary);
                HourlyIcon.Add(item2.icon);
                
            }



            //Using viewbag to pass hourly data to view for use 
            ViewBag.Hourly0 = Day(HourlyTime[0]);
            ViewBag.HourlyDate0 = dateHours(HourlyTime[0]);
            ViewBag.HourlyTime0 = HourlyTime[0];
            ViewBag.HourlySummary0 = HourlySummary[0];
            ViewBag.HourlyIcon0 = HourlyIcon[0];


            ViewBag.HourlyDate1 = Day(HourlyTime[2]);
            ViewBag.HourlyDate1 = dateHours(HourlyTime[2]);
            ViewBag.HourlyTime1 = HourlyTime[2];
            ViewBag.HourlySummary1 = HourlySummary[2];
            ViewBag.HourlyIcon1 = HourlyIcon[2];

            ViewBag.Hourly2 = Day(HourlyTime[4]);
            ViewBag.HourlyDate2 = dateHours(HourlyTime[4]);
            ViewBag.HourlyTime2 = HourlyTime[4];
            ViewBag.HourlySummary2 = HourlySummary[4];
            ViewBag.HourlyIcon2 = HourlyIcon[4];

            ViewBag.Hourly3 = Day(HourlyTime[6]);
            ViewBag.HourlyDate3 = dateHours(HourlyTime[6]);
            ViewBag.HourlyTime3 = HourlyTime[6];
            ViewBag.HourlySummary3 = HourlySummary[6];
            ViewBag.HourlyIcon3 = HourlyIcon[6];

            ViewBag.Hourly4 = Day(HourlyTime[8]);
            ViewBag.HourlyDate4 = dateHours(HourlyTime[8]);
            ViewBag.HourlyTime4 = HourlyTime[8];
            ViewBag.HourlySummary4 = HourlySummary[8];
            ViewBag.HourlyIcon4 = HourlyIcon[8];

            //Not using this data

            //ViewBag.Hourly6 = Day(HourlyTime[10]);
            //ViewBag.HourlyDate6 = dateHours(HourlyTime[10]);
            //ViewBag.HourlyTime6 = HourlyTime[10];
            //ViewBag.HourlySummary6 = HourlySummary[10];
            //ViewBag.HourlyIcon6 = HourlyIcon[10];

            //ViewBag.Hourly7 = Day(HourlyTime[12]);
            //ViewBag.HourlyDate7 = dateHours(HourlyTime[12]);
            //ViewBag.HourlyTime7 = HourlyTime[12];
            //ViewBag.HourlySummary7 = HourlySummary[12];
            //ViewBag.HourlyIcon7 = HourlyIcon[12];





            
            List<DateTime> DailyTime = new List<DateTime>();
            List<string> DailySummary = new List<string>();
            List<string> DailyIcon = new List<string>();


            //Passing daily weather data from controller to view
            foreach (DailyWeatherItem item in dto.daily.data)
            {

                DailyTime.Add(UnixTimeStampToDateTime(item.time));
                DailySummary.Add(item.summary);
                DailyIcon.Add(item.icon);

            }


            //Returning data to view
            ViewBag.Day0 = Day(DailyTime[0]);
            ViewBag.Date0 = date(DailyTime[0]);
            ViewBag.DailyTime0 = DailyTime[0];
            ViewBag.DailySummary0 = DailySummary[0];
            ViewBag.DailyIcon0 = DailyIcon[0];


            ViewBag.Day1 = Day(DailyTime[1]);
            ViewBag.Date1 = date(DailyTime[1]);
            ViewBag.DailyTime1 = DailyTime[1];
            ViewBag.DailySummary1 = DailySummary[1];
            ViewBag.DailyIcon1 = DailyIcon[1];


            ViewBag.Day2 = Day(DailyTime[2]);
            ViewBag.Date2 = date(DailyTime[2]);
            ViewBag.DailyTime2 = DailyTime[2];
            ViewBag.DailySummary2 = DailySummary[2];
            ViewBag.DailyIcon2 = DailyIcon[2];

            ViewBag.Day3 = Day(DailyTime[3]);
            ViewBag.Date3 = date(DailyTime[3]);
            ViewBag.DailyTime3 = DailyTime[3];
            ViewBag.DailySummary3 = DailySummary[3];
            ViewBag.DailyIcon3 = DailyIcon[3];


            ViewBag.Day4 = Day(DailyTime[4]);
            ViewBag.Date4 = date(DailyTime[4]);
            ViewBag.DailyTime4 = DailyTime[4];
            ViewBag.DailySummary4 = DailySummary[4];
            ViewBag.DailyIcon4 = DailyIcon[4];

            ViewBag.Day5 = Day(DailyTime[5]);
            ViewBag.Date5 = date(DailyTime[5]);
            ViewBag.DailyTime5 = DailyTime[5];
            ViewBag.DailySummary5 = DailySummary[5];
            ViewBag.DailyIcon5 = DailyIcon[5];

            ViewBag.Day6 = Day(DailyTime[6]);
            ViewBag.Date6 = date(DailyTime[6]);
            ViewBag.DailyTime6 = DailyTime[6];
            ViewBag.DailySummary6 = DailySummary[6];
            ViewBag.DailyIcon6 = DailyIcon[6];

            ViewBag.Day7 = Day(DailyTime[7]);
            ViewBag.Date7 = date(DailyTime[7]);
            ViewBag.DailyTime7 = DailyTime[7];
            ViewBag.DailySummary7 = DailySummary[7];
            ViewBag.DailyIcon7 = DailyIcon[7];


            


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Connection to API invalid: '{e}'");
            }

            return View();

        }


        [HttpPost]
        public ActionResult Index(Coordinates co, string reportName)
        {

            List<double> Coo = new List<double>();

            Coo = GoogleReverse(reportName);

            
            Coordinates coordinates = new Coordinates();

           
            double Lat = Coo[0]; 
            double Lon = Coo[1];
            

            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/" + Lat + "," + Lon + "?units=si") as HttpWebRequest;

            string rawJSON = "";

            try { 

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                rawJSON = reader.ReadToEnd();
                
            }

            DailyWeatherDTO dto = JsonConvert.DeserializeObject<DailyWeatherDTO>(rawJSON);

            //Timestamp converter method
            DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                DayOfWeek day = dtDateTime.DayOfWeek;

                return dtDateTime;
            }
            
            //returns a string format for use in view
            DayOfWeek Day (DateTime dateTime) {

                //string date = dateTime.ToString("MM:dd:yyyy");
                DayOfWeek day = dateTime.DayOfWeek;

                //datetime DateWithTimeNoSeconds = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                return day;
            }


            //returns a string format for use in view
            String date(DateTime dateTime)
            {

                string D = dateTime.ToString("MM:dd:yyyy");
                string DD = dateTime.ToString("yyyy, MM ,dd");
                //string DJ = dateTime.ToString("yyyy,dd ,MM");
                //DayOfWeek day = dateTime.DayOfWeek;
                
                return DD;
            }


            //returns a string format for use in view
            String dateHours(DateTime dateTime)
            {

                string D = dateTime.ToString("MM:dd:yyyy");
                string value = dateTime.ToString("HHmm");
                string DM = dateTime.ToString("yyyy, MM ,dd ,HHmm, ss");
                string DD = dateTime.ToString("yyyy, MM ,dd ,HH, mm, ss");
                //string DJ = dateTime.ToString("yyyy,dd ,MM");
                //DayOfWeek day = dateTime.DayOfWeek;



                return DD;
            }




            StringBuilder sb = new StringBuilder();

            

            //Returning data to view
            ViewBag.CWindspeed = dto.currently.windSpeed;
            ViewBag.CWindbearing = dto.currently.windBearing;
            ViewBag.CHumidity = dto.currently.humidity*100;
            ViewBag.CDewpoint = dto.currently.dewPoint;
            ViewBag.CUvindex = dto.currently.uvIndex;
            ViewBag.CVisibility = dto.currently.visibility;
            ViewBag.CPressure = dto.currently.pressure;

            ViewBag.Cicon = dto.currently.icon;
            ViewBag.Ctemperature = dto.currently.temperature;

            ViewBag.CSummary = dto.currently.summary;





            List<DateTime> HourlyTime = new List<DateTime>();
            List<string> HourlySummary = new List<string>();
            List<string> HourlyIcon = new List<string>();



            foreach (HourlyWeatherItem item2 in dto.hourly.data)

            {
                //sb.Append("<table><tr><th>Hourly Weather Description</th></tr>");
                ////    //sb.Append("<tr><td>Flags:</td><td>" + dto.flags.units + "</td></tr>");
                //sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item2.time) + "</td></tr>");
                ////    //sb.Append("<tr><td>Time:</td><td>" + item2.time + "</td></tr>");
                //sb.Append("<tr><td>Summary:</td><td>" + item2.summary + "</td></tr>");
                ////    //sb.Append("<tr><td>Time:</td><td>" + item2.icon + "</td></tr>");

                //    sb.Append("<tr><td>Apparent Temperature:</td><td>" + item2.apparentTemperature + "</td></tr>");
                //    //sb.Append("<tr><td>Low:</td><td>" + item2.dewPoint + "</td></tr>");
                //    //sb.Append("<tr><td>Low:</td><td>" + item2.pressure + "</td></tr>");
                //sb.Append("<tr><td>Humidity:</td><td>" + item2.humidity + "</td></tr>");
                //    //sb.Append("<tr><td>Chance of Precipitation:</td><td>" + item2.precipType + "</td></tr>");
                //sb.Append("<tr><td>Wind Speed:</td><td>" + item2.windSpeed + "</td></tr>");
                //sb.Append("</table>");

                HourlyTime.Add(UnixTimeStampToDateTime(item2.time));
                HourlySummary.Add(item2.summary);
                HourlyIcon.Add(item2.icon);

                    
            }

            
            ViewBag.Hourly0 = Day(HourlyTime[0]);
            ViewBag.HourlyDate0 = dateHours(HourlyTime[0]);
            ViewBag.HourlyTime0 = HourlyTime[0];
            ViewBag.HourlySummary0 = HourlySummary[0];
            ViewBag.HourlyIcon0 = HourlyIcon[0];


            ViewBag.HourlyDate1 = Day(HourlyTime[2]);
            ViewBag.HourlyDate1 = dateHours(HourlyTime[2]);
            ViewBag.HourlyTime1 = HourlyTime[2];
            ViewBag.HourlySummary1 = HourlySummary[2];
            ViewBag.HourlyIcon1 = HourlyIcon[2];

            ViewBag.Hourly2 = Day(HourlyTime[4]);
            ViewBag.HourlyDate2 = dateHours(HourlyTime[4]);
            ViewBag.HourlyTime2 = HourlyTime[4];
            ViewBag.HourlySummary2 = HourlySummary[4];
            ViewBag.HourlyIcon2 = HourlyIcon[4];

            ViewBag.Hourly3 = Day(HourlyTime[6]);
            ViewBag.HourlyDate3 = dateHours(HourlyTime[6]);
            ViewBag.HourlyTime3 = HourlyTime[6];
            ViewBag.HourlySummary3 = HourlySummary[6];
            ViewBag.HourlyIcon3 = HourlyIcon[6];

            ViewBag.Hourly4 = Day(HourlyTime[8]);
            ViewBag.HourlyDate4 = dateHours(HourlyTime[8]);
            ViewBag.HourlyTime4 = HourlyTime[8];
            ViewBag.HourlySummary4 = HourlySummary[8];
            ViewBag.HourlyIcon4 = HourlyIcon[8];

            //Not using this data

            //ViewBag.Hourly6 = Day(HourlyTime[10]);
            //ViewBag.HourlyDate6 = dateHours(HourlyTime[10]);
            //ViewBag.HourlyTime6 = HourlyTime[10];
            //ViewBag.HourlySummary6 = HourlySummary[10];
            //ViewBag.HourlyIcon6 = HourlyIcon[10];

            //ViewBag.Hourly7 = Day(HourlyTime[12]);
            //ViewBag.HourlyDate7 = dateHours(HourlyTime[12]);
            //ViewBag.HourlyTime7 = HourlyTime[12];
            //ViewBag.HourlySummary7 = HourlySummary[12];
            //ViewBag.HourlyIcon7 = HourlyIcon[12];


            List<DateTime> DailyTime = new List<DateTime>();
            List<string> DailySummary = new List<string>();
            List<string> DailyIcon = new List<string>();



            foreach (DailyWeatherItem item in dto.daily.data)
            {
                //sb.Append("<table><tr><th>Daily Weather Description</th></tr>");
                ////sb.Append("<tr><td>Flags:</td><td>" + dto.flags.units + "</td></tr>");
                //sb.Append("<tr><td>Time:</td><td>" + UnixTimeStampToDateTime(item.time) + "</td></tr>");
                ////sb.Append("<tr><td>Time:</td><td>" + item.time + "</td></tr>");
                //sb.Append("<tr><td>Summary</td><td>" + item.summary + "</td></tr>");
                //sb.Append("<tr><td>Icon:</td><td>" + item.icon + "</td></tr>");
                ////sb.Append("<tr><td>High:</td><td>" + item.temperatureMax + "</td></tr>");
                ////sb.Append("<tr><td>Low:</td><td>" + item.temperatureLow + "</td></tr>");
                ////sb.Append("<tr><td>Humidity:</td><td>" + item.humidity + "</td></tr>");
                ////sb.Append("<tr><td>Low:</td><td>" + item.pressure + "</td></tr>");
                ////sb.Append("<tr><td>Chance of Precipitation:</td><td>" + item.precipType + "</td></tr>");
                ////sb.Append("<tr><td>Wind Speed:</td><td>" + item.windSpeed + "</td></tr>");
                //sb.Append("</table>");

                DailyTime.Add(UnixTimeStampToDateTime(item.time));
                DailySummary.Add(item.summary);
                DailyIcon.Add(item.icon);

            }


            ViewBag.Day0 = Day(DailyTime[0]);
            ViewBag.Date0 = date(DailyTime[0]);
            ViewBag.DailyTime0 = DailyTime[0];
            ViewBag.DailySummary0 = DailySummary[0];
            ViewBag.DailyIcon0 = DailyIcon[0];


            ViewBag.Day1 = Day(DailyTime[1]);
            ViewBag.Date1 = date(DailyTime[1]);
            ViewBag.DailyTime1 = DailyTime[1];
            ViewBag.DailySummary1 = DailySummary[1];
            ViewBag.DailyIcon1 = DailyIcon[1];


            ViewBag.Day2 = Day(DailyTime[2]);
            ViewBag.Date2 = date(DailyTime[2]);
            ViewBag.DailyTime2 = DailyTime[2];
            ViewBag.DailySummary2 = DailySummary[2];
            ViewBag.DailyIcon2 = DailyIcon[2];

            ViewBag.Day3 = Day(DailyTime[3]);
            ViewBag.Date3 = date(DailyTime[3]);
            ViewBag.DailyTime3 = DailyTime[3];
            ViewBag.DailySummary3 = DailySummary[3];
            ViewBag.DailyIcon3 = DailyIcon[3];

            
            ViewBag.Day4 = Day(DailyTime[4]);
            ViewBag.Date4 = date(DailyTime[4]);
            ViewBag.DailyTime4 = DailyTime[4];
            ViewBag.DailySummary4 = DailySummary[4];
            ViewBag.DailyIcon4 = DailyIcon[4];

            ViewBag.Day5 = Day(DailyTime[5]);
            ViewBag.Date5 = date(DailyTime[5]);
            ViewBag.DailyTime5 = DailyTime[5];
            ViewBag.DailySummary5 = DailySummary[5];
            ViewBag.DailyIcon5 = DailyIcon[5];

            ViewBag.Day6 = Day(DailyTime[6]);
            ViewBag.Date6 = date(DailyTime[6]);
            ViewBag.DailyTime6 = DailyTime[6];
            ViewBag.DailySummary6 = DailySummary[6];
            ViewBag.DailyIcon6 = DailyIcon[6];

            ViewBag.Day7 = Day(DailyTime[7]);
            ViewBag.Date7 = date(DailyTime[7]);
            ViewBag.DailyTime7 = DailyTime[7];
            ViewBag.DailySummary7 = DailySummary[7];
            ViewBag.DailyIcon7 = DailyIcon[7];





            //coordinates.rawJSON = sb.ToString();


            string a = sb.ToString();
            ViewBag.a = a;


        }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Connection to API invalid: '{e}'");
            }
            

            return View(coordinates);
        }

        
        public List<double> GoogleReverse(string reportName)
        {

            string address = reportName;

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
            List<double> coordinates = new List<double>();
            
            
            foreach (Result item in geo.results)
            {
                
                coordinates.Add(item.geometry.location.lat);
                coordinates.Add(item.geometry.location.lng);
                
                break;

            }

            
            return(coordinates);
        }
    }



    //Takes in coordinates and Returns an address
    //        [HttpGet]
    //        public ActionResult Index2()
    //        {


    //        //long Lat = co.Latitude;
    //        //long Lon = co.Longitude;

    //        double Lat = 53.2707;
    //        double Lon = -9.0568;

    //        string address = "18 Bridgewter court, galway, irleland";


    //        HttpWebRequest apiRequest = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + Lat + "," + Lon + "&key=AIzaSyCY4XX-om0CBwXnmF0XbqM2M1kiUcOsZ3Q") as HttpWebRequest;

    //        //string rawJSON = "";
    //        string apiResponse = "";

    //        using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
    //        {
    //            StreamReader reader = new StreamReader(response.GetResponseStream());
    //            apiResponse = reader.ReadToEnd();

    //        }


    //        RootObject geo = JsonConvert.DeserializeObject<RootObject>(apiResponse);

    //        StringBuilder sb = new StringBuilder();

    //        sb.Append(geo.status);

    //        foreach (Result item in geo.results)
    //        {
    //            sb.Append(item.formatted_address);

    //        }

    //       string a = sb.ToString();

    //        ViewBag.a = a;

    //        return PartialView();
    //    }
    //}

    //Takes in a address and returns coordinates
    //[HttpPost]
    //public ActionResult Index(string reportName)
    //{

    //    string address = reportName;

    //    //"18 + Bridgewter + court + galway + irleland"

    //    //string address = "galway irleland";
    //    //string address = "1600 + Amphitheatre + Parkway,+Mountain + View,+CA";
    //    HttpWebRequest apiRequest = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyCY4XX-om0CBwXnmF0XbqM2M1kiUcOsZ3Q") as HttpWebRequest;

    //    //string rawJSON = "";
    //    string apiResponse = "";

    //    using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
    //    {
    //        StreamReader reader = new StreamReader(response.GetResponseStream());
    //        apiResponse = reader.ReadToEnd();

    //    }


    //    RootObject geo = JsonConvert.DeserializeObject<RootObject>(apiResponse);

    //    StringBuilder sb = new StringBuilder();

    //    sb.Append(geo.status);


    //    //created a list to take in values from my object
    //    List<double> c = new List<double>();
    //    //c.Add(12345.23);

    //    double elem;        //Declaring elements to store lat and long
    //    double elem2;

    //    foreach (Result item in geo.results)
    //    {
    //        //sb.Append(item.geometry.location.lat);
    //        //sb.Append(item.geometry.location.lat);

    //        elem = item.geometry.location.lat;
    //        elem2 = item.geometry.location.lng;

    //        //TempData["CoordinatesLat"] = item.geometry.location.lat;
    //        //TempData["CoordinatesLng"] = item.geometry.location.lng;

    //        c.Add(item.geometry.location.lat);
    //        c.Add(item.geometry.location.lng);


    //        break;

    //    }


    //    double e = c[0];
    //    double f = c[1];



    //    //int elem = intList[1];

    //    //geo.results[1];         


    //    //public int IndexOf(T item, int index);

    //    //for ( int z=0; z < geo.results.Count; z++) {


    //    //    //geometry.location.lat;
    //    //    int elem = 

    //    //}

    //    //TempData["Coordinates"] =

    //    //string a = tempdata["coordinateslat"];

    //    //TempData["Employee"] = employee;

    //    string a = sb.ToString();

    //    //ViewBag.a = a;



    //    return PartialView();
    //}

}


//https://www.c-sharpcorner.com/article/climate-report-information-using-asp-net-mvc-and-api-key/