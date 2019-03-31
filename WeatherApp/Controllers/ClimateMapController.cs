using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;   //HttpWebRequest & HttpWebResponse files, HttpWebRequest = This class provides http specific implementation of WebRequest class.  WebRequest = This abstract class makes a request to uniform resource identifier.
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

        //public string Index()
        //{
        //    return "Hello";
        //}

        public ActionResult Index()
        {
            Coordinates openWeatherMap = new Coordinates();

            //string a = openWeatherMap.rawJSON;

            //ViewBag.a = a;


            //return View(openWeatherMap.rawJSON);
            //return RedirectToAction("Index2");
            return View();

        }


        //Takes input from the html page in ajax anbd returns it to the model and thne trhe controller
        //public ActionResult Coordinates(Coordinates co)
        //{

        //    //return null;
        //    return null;
        //}


        //public ActionResult Report()
        //{
        //    return View(new Coordinates());
        //    //return RedirectToAction("Index2");

        //}

        //public ActionResult Report(string reportName)
        //{
        //    return View(new Coordinates());
        //}

        //public ActionResult Index()
        //{
        //    Coordinates openWeatherMap = FillCo();       //Is just returning the fill city method
        //    return View(openWeatherMap);
        //}



        //public ActionResult PartialViewExample()
        //public ActionResult Index(Coordinates model)

        //[HttpGet]
        [HttpPost]
        public ActionResult Index(Coordinates co, string reportName)
        //public ActionResult Index(string reportName)

        {

            List<double> Coo = new List<double>();

            Coo = GoogleReverse(reportName);

            //Coordinates openWeatherMap = new Coordinates();


            Coordinates coordinates = new Coordinates();

            //DarkSkiApi Dark = new DarkSkiApi();


            double Lat = Coo[0]; 
            double Lon = Coo[1];


            //long Lat = co.Latitude;
            //long Lon = co.Longitude;

            //double Lat = 53.2707;
            //double Lon = -9.0568;



            HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/" + Lat + "," + Lon + "?units=si") as HttpWebRequest;

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



        //public Coordinates FillCo()  //function using model file 
        //{
        //    Coordinates openWeatherMap = new Coordinates();
        //    openWeatherMap.co = new Dictionary<long, long>();
        //    openWeatherMap.co.Add(5, -4);
        //    return openWeatherMap;
        //}




        //public Coordinates co()
        //{
        //    Coordinates coordinates = new Coordinates();

        //    long a = coordinates.Latitude;
        //    long b = coordinates.Longitude;


        //    return coordinates;

        //}

        //    public ClimateModel FillCity()  //function using model file 
        //    {
        //        ClimateModel openWeatherMap = new ClimateModel();
        //        openWeatherMap.cities = new Dictionary<string, string>();
        //        openWeatherMap.cities.Add("Dublin", "7778677");
        //        openWeatherMap.cities.Add("London", "2643743");
        //        return openWeatherMap;
        //    }


        //[HttpPost]
        //public String Index3()
        //{

        //    HttpWebRequest apiRequest = WebRequest.Create("https://api.darksky.net/forecast/96fa91be980998a140c62d770883e863/53.2707,-9.0568") as HttpWebRequest;

        //    string rawJSON = "";
        //    using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
        //    {
        //        StreamReader reader = new StreamReader(response.GetResponseStream());
        //        rawJSON = reader.ReadToEnd();
        //    }


        //    DailyWeatherDTO dto = JsonConvert.DeserializeObject<DailyWeatherDTO>(rawJSON);



        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<table><tr><th>Weather Description</th></tr>");

        //    rawJSON = sb.ToString();

        //    //System.Diagnostics.Debug.WriteLine("hello ");

        //    return rawJSON;    //Passing model class object
        //}

        //public DarkSkiModel Fill()  //function using model file 
        //{
        //    DarkSkiModel DailyWeatherDTO = new DarkSkiModel();
        //    DailyWeatherDTO.cities = new Dictionary<string, string>();
        //    DailyWeatherDTO.cities.Add("Dublin", "7778677");
        //    DailyWeatherDTO.cities.Add("London", "2643743");
        //    return DailyWeatherDTO;
        //}





        //    public ActionResult Index()
        //    {
        //        ClimateModel openWeatherMap = FillCity();       //Is just returning the fill city method
        //        return View(openWeatherMap);
        //    }

        //    public ActionResult Index2()
        //    {
        //        ClimateModel openWeatherMap = FillCity();       //Is just returning the fill city method
        //        return View(openWeatherMap);
        //    }

        //    [HttpPost]
        //    public ActionResult Index(string cities)
        //    {
        //        ClimateModel openWeatherMap = FillCity();

        //        if (cities != null)
        //        {

        //            string apiKey = "6c2682c25ef73872434b1c6edb522d9b";
        //            HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" + cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

        //            string apiResponse = "";
        //            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
        //            {
        //                StreamReader reader = new StreamReader(response.GetResponseStream());
        //                apiResponse = reader.ReadToEnd();
        //            }

        //            ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("<table><tr><th>Weather Description</th></tr>");
        //            sb.Append("<tr><td>City:</td><td>" + rootObject.name + "</td></tr>");
        //            sb.Append("<tr><td>Country:</td><td>" + rootObject.sys.country + "</td></tr>");
        //            sb.Append("<tr><td>Country Sun Rise:</td><td>" + rootObject.sys.sunrise + "</td></tr>");
        //            sb.Append("<tr><td>Country Sun Sete:</td><td>" + rootObject.sys.sunset + "</td></tr>");
        //            sb.Append("<tr><td>Wind:</td><td>" + rootObject.wind.speed + " Km/h</td></tr>");
        //            sb.Append("<tr><td>Current Temperature:</td><td>" + rootObject.main.temp + " °C</td></tr>");
        //            sb.Append("<tr><td>Max. Temperature:</td><td>" + rootObject.main.temp_max + " °C</td></tr>");
        //            sb.Append("<tr><td>Min. Temperature:</td><td>" + rootObject.main.temp_min + " °C</td></tr>");
        //            sb.Append("<tr><td>Pressure:</td><td>" + rootObject.main.pressure + "</td></tr>");
        //            sb.Append("<tr><td>Humidity:</td><td>" + rootObject.main.humidity + "</td></tr>");
        //            sb.Append("<tr><td>Weather:</td><td>" + rootObject.weather[0].description + "</td></tr>");
        //            sb.Append("</table>");
        //            openWeatherMap.apiResponse = sb.ToString();
        //        }
        //        else
        //        {
        //            if (Request.Form["submit"] != null)
        //            {
        //                TempData["SelectOption"] = -1;
        //            }
        //        }
        //        return View(openWeatherMap);    //Passing model class object
        //    }


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

//https://www.c-sharpcorner.com/article/climate-report-information-using-asp-net-mvc-and-api-key/