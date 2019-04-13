using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class News
    {
        //public int Id { get; set; }
        public string Headline { get; set; }
        public int Date { get; set; }
        public string Lead { get; set; }
        public string Snippet { get; set; }
        public string Web_url { get; set; }


        //public IEnumerable <News> news;

        //public News(string headline, int date)        
        //{
        //    Headline = headline;
        //    Date = date;

        //}

        public News()
        {
        }
}

}