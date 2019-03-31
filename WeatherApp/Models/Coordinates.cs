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

        public string rawJSON { get; set; }

        //public Dictionary<long, long> co    //Hashset in java
        //{
        //    get;
        //    set;
        //}

    }

}