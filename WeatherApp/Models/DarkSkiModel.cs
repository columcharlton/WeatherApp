using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class DarkSkiModel
    {
        public string apiDSResponse { get; set; }

        public Dictionary<string, string> cities    //Hashset in java
        {
            get;
            set;
        }
    }
}