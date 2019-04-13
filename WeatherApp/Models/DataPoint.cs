using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace WeatherApp.Models
{

    ////DataContract for Serializing Data - required to serve in JSON format
    //[DataContract]
    //public class DataPoint
    //{
    //    public DataPoint(double x, double y)
    //    {
    //        this.X = x;
    //        this.Y = y;
    //    }

    //    //Explicitly setting the name to be used while serializing to JSON.
    //    [DataMember(Name = "x")]
    //    public Nullable<double> X = null;

    //    //Explicitly setting the name to be used while serializing to JSON.
    //    [DataMember(Name = "y")]
    //    public Nullable<double> Y = null;
    //}

    //DataContract for Serializing Data - required to serve in JSON format
    //[DataContract]
    //public class DataPoint
    //{
    //    public DataPoint(string label, double y)
    //    {
    //        this.Label = label;
    //        this.Y = y;
    //    }

    //    //Explicitly setting the name to be used while serializing to JSON.
    //    [DataMember(Name = "label")]
    //    public string Label = "";

    //    //Explicitly setting the name to be used while serializing to JSON.
    //    [DataMember(Name = "y")]
    //    public Nullable<double> Y = null;
    //}
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint
    {
        public DataPoint(double x, dynamic y)
        {
            this.X = x;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public Nullable<double> X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public dynamic Y = null;
    }
}
