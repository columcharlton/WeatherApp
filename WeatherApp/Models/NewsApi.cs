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

    //public class Headline
    //{
    //    public string main { get; set; }
    //}

    //public class Keyword
    //{
    //    public string name { get; set; }
    //    public string value { get; set; }
    //}

    //public class Person
    //{
    //    public string organization { get; set; }
    //    public string role { get; set; }
    //    public string firstname { get; set; }
    //    public int rank { get; set; }
    //    public string lastname { get; set; }
    //}

    //public class Byline
    //{
    //    public List<Person> person { get; set; }
    //    public string original { get; set; }
    //}

    //public class __invalid_type__0
    //{
    //    public string web_url { get; set; }
    //    public string snippet { get; set; }
    //    public string lead_paragraph { get; set; }
    //    public object @abstract { get; set; }
    //    public string print_page { get; set; }
    //    public List<object> blog { get; set; }
    //    public string source { get; set; }
    //    public List<object> multimedia { get; set; }
    //    public Headline headline { get; set; }
    //    public List<Keyword> keywords { get; set; }
    //    public DateTime pub_date { get; set; }
    //    public string document_type { get; set; }
    //    public string news_desk { get; set; }
    //    public string section_name { get; set; }
    //    public object subsection_name { get; set; }
    //    public Byline byline { get; set; }
    //    public string type_of_material { get; set; }
    //    public string _id { get; set; }
    //    public int word_count { get; set; }
    //    public object slideshow_credits { get; set; }
    //}

    //public class Headline2
    //{
    //    public string main { get; set; }
    //}

    //public class Keyword2
    //{
    //    public string name { get; set; }
    //    public string value { get; set; }
    //}

    //public class Person2
    //{
    //    public string organization { get; set; }
    //    public string role { get; set; }
    //    public string firstname { get; set; }
    //    public int rank { get; set; }
    //    public string lastname { get; set; }
    //}

    //public class Byline2
    //{
    //    public List<Person2> person { get; set; }
    //    public string original { get; set; }
    //}

    //public class __invalid_type__1
    //{
    //    public string web_url { get; set; }
    //    public string snippet { get; set; }
    //    public string lead_paragraph { get; set; }
    //    public object @abstract { get; set; }
    //    public string print_page { get; set; }
    //    public List<object> blog { get; set; }
    //    public string source { get; set; }
    //    public List<object> multimedia { get; set; }
    //    public Headline2 headline { get; set; }
    //    public List<Keyword2> keywords { get; set; }
    //    public DateTime pub_date { get; set; }
    //    public string document_type { get; set; }
    //    public string news_desk { get; set; }
    //    public string section_name { get; set; }
    //    public object subsection_name { get; set; }
    //    public Byline2 byline { get; set; }
    //    public string type_of_material { get; set; }
    //    public string _id { get; set; }
    //    public int word_count { get; set; }
    //    public object slideshow_credits { get; set; }
    //}

    //public class Headline3
    //{
    //    public string main { get; set; }
    //}

    //public class Keyword3
    //{
    //    public string name { get; set; }
    //    public string value { get; set; }
    //}

    //public class Person3
    //{
    //    public string organization { get; set; }
    //    public string role { get; set; }
    //    public string firstname { get; set; }
    //    public int rank { get; set; }
    //    public string lastname { get; set; }
    //}

    //public class Byline3
    //{
    //    public List<Person3> person { get; set; }
    //    public string original { get; set; }
    //}

    //public class __invalid_type__2
    //{
    //    public string web_url { get; set; }
    //    public string snippet { get; set; }
    //    public string lead_paragraph { get; set; }
    //    public object @abstract { get; set; }
    //    public string print_page { get; set; }
    //    public List<object> blog { get; set; }
    //    public string source { get; set; }
    //    public List<object> multimedia { get; set; }
    //    public Headline3 headline { get; set; }
    //    public List<Keyword3> keywords { get; set; }
    //    public DateTime pub_date { get; set; }
    //    public string document_type { get; set; }
    //    public string news_desk { get; set; }
    //    public string section_name { get; set; }
    //    public object subsection_name { get; set; }
    //    public Byline3 byline { get; set; }
    //    public string type_of_material { get; set; }
    //    public string _id { get; set; }
    //    public int word_count { get; set; }
    //    public object slideshow_credits { get; set; }
    //}


    //public class Docs2
    //{
        
    //    public __invalid_type__0 __invalid_name__0 { get; set; }
    //    public __invalid_type__1 __invalid_name__1 { get; set; }
    //    public __invalid_type__2 __invalid_name__2 { get; set; }
    //}






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