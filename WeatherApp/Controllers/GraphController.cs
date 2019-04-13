using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WeatherApp.Class;
using WeatherApp.Models;


namespace WeatherApp.Controllers
{
    public class GraphController : Controller
    {
        //// GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //    public ContentResult JSON()
        //    {
        //        List<DataPoint> dataPoints = new List<DataPoint>();


        //        dataPoints.Add(new DataPoint(1481999400000, 4.67));
        //        dataPoints.Add(new DataPoint(1482604200000, 4.7));
        //        dataPoints.Add(new DataPoint(1483209000000, 4.96));
        //        dataPoints.Add(new DataPoint(1483813800000, 5.12));
        //        dataPoints.Add(new DataPoint(1484418600000, 5.08));
        //        dataPoints.Add(new DataPoint(1485023400000, 5.11));
        //        dataPoints.Add(new DataPoint(1485628200000, 5));
        //        dataPoints.Add(new DataPoint(1486233000000, 5.2));
        //        dataPoints.Add(new DataPoint(1486837800000, 4.7));
        //        dataPoints.Add(new DataPoint(1487442600000, 4.74));
        //        dataPoints.Add(new DataPoint(1488047400000, 4.67));
        //        dataPoints.Add(new DataPoint(1488652200000, 4.66));
        //        dataPoints.Add(new DataPoint(1489257000000, 4.86));
        //        dataPoints.Add(new DataPoint(1489861800000, 4.91));
        //        dataPoints.Add(new DataPoint(1490466600000, 5.12));
        //        dataPoints.Add(new DataPoint(1491071400000, 5.4));
        //        dataPoints.Add(new DataPoint(1491676200000, 5.08));
        //        dataPoints.Add(new DataPoint(1492281000000, 5.05));
        //        dataPoints.Add(new DataPoint(1492885800000, 4.98));
        //        dataPoints.Add(new DataPoint(1493490600000, 4.89));
        //        dataPoints.Add(new DataPoint(1494095400000, 4.9));
        //        dataPoints.Add(new DataPoint(1494700200000, 4.95));
        //        dataPoints.Add(new DataPoint(1495305000000, 4.88));
        //        dataPoints.Add(new DataPoint(1495909800000, 5.07));
        //        dataPoints.Add(new DataPoint(1496514600000, 5.14));
        //        dataPoints.Add(new DataPoint(1497119400000, 5.05));
        //        dataPoints.Add(new DataPoint(1497724200000, 5.03));
        //        dataPoints.Add(new DataPoint(1498329000000, 4.93));
        //        dataPoints.Add(new DataPoint(1498933800000, 4.97));
        //        dataPoints.Add(new DataPoint(1499538600000, 4.86));
        //        dataPoints.Add(new DataPoint(1500143400000, 4.95));
        //        dataPoints.Add(new DataPoint(1500748200000, 4.83));
        //        dataPoints.Add(new DataPoint(1501353000000, 4.83));
        //        dataPoints.Add(new DataPoint(1501957800000, 4.73));
        //        dataPoints.Add(new DataPoint(1502562600000, 4.56));
        //        dataPoints.Add(new DataPoint(1503167400000, 4.34));
        //        dataPoints.Add(new DataPoint(1503772200000, 4.25));
        //        dataPoints.Add(new DataPoint(1504377000000, 4.18));
        //        dataPoints.Add(new DataPoint(1504981800000, 4.22));
        //        dataPoints.Add(new DataPoint(1505586600000, 4.18));
        //        dataPoints.Add(new DataPoint(1506191400000, 4.31));
        //        dataPoints.Add(new DataPoint(1506796200000, 4.34));
        //        dataPoints.Add(new DataPoint(1507401000000, 4.47));
        //        dataPoints.Add(new DataPoint(1508005800000, 4.57));
        //        dataPoints.Add(new DataPoint(1508610600000, 4.63));
        //        dataPoints.Add(new DataPoint(1509215400000, 4.55));
        //        dataPoints.Add(new DataPoint(1509820200000, 4.55));
        //        dataPoints.Add(new DataPoint(1510425000000, 4.44));
        //        dataPoints.Add(new DataPoint(1511029800000, 4.46));
        //        dataPoints.Add(new DataPoint(1511634600000, 4.41));
        //        dataPoints.Add(new DataPoint(1512239400000, 4.3));
        //        dataPoints.Add(new DataPoint(1512844200000, 4.31));
        //        dataPoints.Add(new DataPoint(1513449000000, 4.3));
        //        dataPoints.Add(new DataPoint(1513621800000, 4.36));


        //        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
        //        { NullValueHandling = NullValueHandling.Ignore };
        //        return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        //    }
        //}


        // GET: Home
        public ActionResult Index()
        {

            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();



            //long Lat = co.Latitude;
            //long Lon = co.Longitude;

            double Lat = 53.2707;
            double Lon = -9.0568;



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

            double UnixTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch

                double dtDateTime = unixTimeStamp*1000;
                return dtDateTime;
            }




            List<DataPoint> dataPoints = new List<DataPoint>();


            foreach (HourlyWeatherItem item2 in dto.hourly.data)

            {


                //dataPoints1.Add(new DataPoint(item2.time, item2.pressure));
                //dataPoints2.Add(new DataPoint(item2.time, new double[] { item2.temperature, item2.apparentTemperature }));
                //dataPoints3.Add(new DataPoint(item2.time, ));

                //{ "x":328238848,"y":[8.56,4.96]},
                //{"x":1555106400,"y":[8.56,4.96],

                //dataPoints1.Add(new DataPoint(1512441720000, 1554937200.0, item2.pressure));
                dataPoints.Add(new DataPoint(UnixTime(item2.time), new double[] { item2.temperature, item2.apparentTemperature }));
                //dataPoints.Add(new DataPoint(item2.time, new double[] { item2.temperature, item2.apparentTemperature }));



            }


            //JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
            //{ NullValueHandling = NullValueHandling.Ignore };



            //return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");


            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            //ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints2);
            //ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);



            //List<DataPoint> dataPoints = new List<DataPoint>();

            //dataPoints.Add(new DataPoint(1512441720000, new double[] { 120, 78 }));
            //dataPoints.Add(new DataPoint(1512447240000, new double[] { 139, 89 }));
            //dataPoints.Add(new DataPoint(1512453960000, new double[] { 139, 85 }));
            //dataPoints.Add(new DataPoint(1512461700000, new double[] { 150, 95 }));
            //dataPoints.Add(new DataPoint(1512466320000, new double[] { 130, 84 }));
            //dataPoints.Add(new DataPoint(1512472020000, new double[] { 138, 87 }));
            //dataPoints.Add(new DataPoint(1512475440000, new double[] { 127, 78 }));
            //dataPoints.Add(new DataPoint(1512477540000, new double[] { 119, 76 }));
            //dataPoints.Add(new DataPoint(1512482280000, new double[] { 135, 82 }));
            //dataPoints.Add(new DataPoint(1512486900000, new double[] { 122, 78 }));   
            //dataPoints.Add(new DataPoint(1512490800000, new double[] { 115, 72 }));
            //dataPoints.Add(new DataPoint(1512494160000, new double[] { 122, 75 }));

            //ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints);

            //dataPoints = new List<DataPoint>();
            //dataPoints.Add(new DataPoint(1512441720000, 36.2));
            //dataPoints.Add(new DataPoint(1512447240000, 36.5));
            //dataPoints.Add(new DataPoint(1512453960000, 36.5));
            //dataPoints.Add(new DataPoint(1512461700000, 36.4));
            //dataPoints.Add(new DataPoint(1512466320000, 36.7));
            //dataPoints.Add(new DataPoint(1512472020000, 36.8));
            //dataPoints.Add(new DataPoint(1512475440000, 36.7));
            //dataPoints.Add(new DataPoint(1512477540000, 36.9));
            //dataPoints.Add(new DataPoint(1512482280000, 37.1));
            //dataPoints.Add(new DataPoint(1512486900000, 37.2));
            //dataPoints.Add(new DataPoint(1512490800000, 37.2));
            //dataPoints.Add(new DataPoint(1512494160000, 37));

            //ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints);





            return View();

        }

        //GET: Home
        //public ActionResult Index()
        //{
        //    List<DataPoint> dataPoints = new List<DataPoint>();

        //    dataPoints.Add(new DataPoint(1512441720000, new double[] { 120, 78 }));
        //    dataPoints.Add(new DataPoint(1512447240000, new double[] { 139, 89 }));
        //    dataPoints.Add(new DataPoint(1512453960000, new double[] { 139, 85 }));
        //    dataPoints.Add(new DataPoint(1512461700000, new double[] { 150, 95 }));
        //    dataPoints.Add(new DataPoint(1512466320000, new double[] { 130, 84 }));
        //    dataPoints.Add(new DataPoint(1512472020000, new double[] { 138, 87 }));
        //    dataPoints.Add(new DataPoint(1512475440000, new double[] { 127, 78 }));
        //    dataPoints.Add(new DataPoint(1512477540000, new double[] { 119, 76 }));
        //    dataPoints.Add(new DataPoint(1512482280000, new double[] { 135, 82 }));
        //    dataPoints.Add(new DataPoint(1512486900000, new double[] { 122, 78 }));
        //    dataPoints.Add(new DataPoint(1512490800000, new double[] { 115, 72 }));
        //    dataPoints.Add(new DataPoint(1512494160000, new double[] { 122, 75 }));

        //    ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints);

        //    dataPoints = new List<DataPoint>();
        //    dataPoints.Add(new DataPoint(1512441720000, 36.2));
        //    dataPoints.Add(new DataPoint(1512447240000, 36.5));
        //    dataPoints.Add(new DataPoint(1512453960000, 36.5));
        //    dataPoints.Add(new DataPoint(1512461700000, 36.4));
        //    dataPoints.Add(new DataPoint(1512466320000, 36.7));
        //    dataPoints.Add(new DataPoint(1512472020000, 36.8));
        //    dataPoints.Add(new DataPoint(1512475440000, 36.7));
        //    dataPoints.Add(new DataPoint(1512477540000, 36.9));
        //    dataPoints.Add(new DataPoint(1512482280000, 37.1));
        //    dataPoints.Add(new DataPoint(1512486900000, 37.2));
        //    dataPoints.Add(new DataPoint(1512490800000, 37.2));
        //    dataPoints.Add(new DataPoint(1512494160000, 37));

        //    ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints);

        //    return View();
        //}
    }
}   
