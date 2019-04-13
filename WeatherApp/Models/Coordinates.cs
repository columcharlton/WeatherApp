using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class Coordinates
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public long Time2 { get; set; }
        public double Pressure2 { get; set; }


        public List<double> Time { get; set; }
        public List<double> Pressure { get; set; }



        public List<Coordinates> Data { get; set; }


        public string rawJSON { get; set; }

        public DateTime dtmDate { get; set; }

        //public Dictionary<long, long> co    //Hashset in java
        //{
        //    get;
        //    set;
        //}

    }

}