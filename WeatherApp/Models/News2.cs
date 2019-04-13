using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public static class News2
    {

        private static List<News> news;


        static News2()
        {
            news = new List<News>();
            // some test data
            //news.Add(new News { Headline = "Foo", Date = 21 });
            // ...
        }
        public static void AddNews(string p, int q)
        {
            //news.Add(p);
        }
        public static List<News> GetNews()
        {
            return news;
        }

    }
}
