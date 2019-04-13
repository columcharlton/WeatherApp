using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Controllers;

namespace WeatherApp.Controllers
{
    public class NewsApi
    {
    }

    public class Meta
    {
        public int hits { get; set; }
    }

    

    public class Headline3
    {
        public string main { get; set; }
    }

    public class Keyword3
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Person3
    {
        public string organization { get; set; }
        public string role { get; set; }
        public string firstname { get; set; }
        public int rank { get; set; }
        public string lastname { get; set; }
    }

    public class Byline3
    {
        public List<Person3> person { get; set; }
        public string original { get; set; }
    }


    public class Docs
    {
        public string web_url { get; set; }
        public string snippet { get; set; }
        public string lead_paragraph { get; set; }
        public Headline3 headline { get; set; }
        public DateTime pub_date { get; set; }

    }

    public class Response
    {
        public Meta meta { get; set; }
        public List< Docs> docs { get; set; }
    }

public class RootObject2
{
    public string copyright { get; set; }
    public Response response { get; set; }
        
    }



}