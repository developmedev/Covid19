using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CoronavirusSOS.RestService
{
    public class RestApi
    {
        HttpClient httpclient;
        public RestApi()
        {
            httpclient = new HttpClient();
        }

        public List<string> GetVsAsync()
        {
            var count = 3;
            List<string> values = new List<string>();
            var weburl = "https://www.worldometers.info/coronavirus/";
            var response = httpclient.GetAsync(weburl).GetAwaiter().GetResult();
            string jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(jsonString);
            var text = "";
            foreach (var HtmlNode in doc.DocumentNode.ChildNodes)
            {
                if (HtmlNode.Name == "html")
                {
                    foreach (var nodehtml in HtmlNode.ChildNodes)
                    {
                        if (nodehtml.Name == "body")
                        {
                            foreach (var n in nodehtml.ChildNodes)
                            {
                                if (n.Name == "div")
                                {
                                    foreach (var i in n.ChildNodes)
                                    {
                                        if (i.Name == "div")
                                        {
                                            foreach (var f in n.ChildNodes)
                                            {
                                                if (f.Name == "div")
                                                {
                                                    foreach (var e in f.ChildNodes)
                                                    {
                                                        if (e.Id == "maincounter-wrap")
                                                        {
                                                            foreach (var g in e.ChildNodes)
                                                            {
                                                                if (g.Name == "div")
                                                                {
                                                                    foreach (var h in g.ChildNodes)
                                                                    {
                                                                        if (h.Name == "span" && count > 0)
                                                                        {
                                                                            if (count == 3)
                                                                            {
                                                                                values.Add(h.InnerText.ToString());
                                                                            }
                                                                            if (count == 2)
                                                                            {
                                                                                values.Add(h.InnerText.ToString());
                                                                            }
                                                                            if (count == 1)
                                                                            {
                                                                                values.Add(h.InnerText.ToString());
                                                                            }
                                                                            count--;


                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            //text += e.InnerHtml.ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }

                        }

                    }

                }


            }
            return values;
        }
        public List<Update> GetUpdatesAsync()
        {

            List<Update> values = new List<Update>();
            var weburl = "https://www.worldometers.info/coronavirus/";
            var response = httpclient.GetAsync(weburl).GetAwaiter().GetResult();
            string jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(jsonString);
            foreach (var HtmlNode in doc.DocumentNode.ChildNodes)
            {
                if (HtmlNode.Name == "html")
                {
                    foreach (var nodehtml in HtmlNode.ChildNodes)
                    {
                        if (nodehtml.Name == "body")
                        {
                            foreach (var n in nodehtml.ChildNodes)
                            {
                                if (n.Name == "div")
                                {
                                    foreach (var i in n.ChildNodes)
                                    {
                                        if (i.Name == "div")
                                        {
                                            foreach (var f in n.ChildNodes)
                                            {
                                                if (f.Name == "div")
                                                {
                                                    foreach (var e in f.ChildNodes)
                                                    {
                                                        if (e.Id == "innercontent")
                                                        {
                                                            foreach (var g in e.ChildNodes)
                                                            {
                                                                if (g.Id == getIdToday())
                                                                {
                                                                    foreach(var t in e.ChildNodes)
                                                                    {
                                                                        if (t.Name == "ul")
                                                                        {
                                                                            foreach (var x in t.ChildNodes)
                                                                            {

                                                                                if (x.Name == "li")
                                                                                {
                                                                                    var url = "";
                                                                                    foreach(var r in x.ChildNodes)
                                                                                    {
                                                                                        if (r.Name == "span")
                                                                                        {
                                                                                            foreach(var s in r.ChildNodes)
                                                                                            {
                                                                                                if (s.Name == "a")
                                                                                                {
                                                                                                    url = s.GetAttributeValue("href","");
                                                                                                   
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    values.Add(new Update() { Id = Guid.NewGuid().ToString(), Text = x.InnerText.Replace("[source]", ""), Url = url });
                                                                                }

                                                                            }
                                                                        }
                                                                    }
                                                                  
                                                                   
                                                                }
                                                            }
                                                            //text += e.InnerHtml.ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }

                        }

                    }

                }


            }
            return values;
        }
        public string getIdToday()
        {
            var date = DateTime.Today;
            var month = date.Month;
            var day = date.Day;
            var monthstring = "";
            var daystring = ""+day;
            switch (month)
            {
                case 1:
                    monthstring = "jan";
                    break;
                case 2:
                    monthstring = "feb";
                    break;
                case 3:
                    monthstring = "mar";
                    break;
                case 4:
                    monthstring = "apr";
                    break;
                case 5:
                    monthstring = "may";
                    break;
                case 6:
                    monthstring = "june";
                    break;
                case 7:
                    monthstring = "july";
                    break;
                case 8:
                    monthstring = "aug";
                    break;
                case 9:
                    monthstring = "sep";
                    break;
                case 10:
                    monthstring = "oct";
                    break;
                case 11:
                    monthstring = "nov";
                    break;
                case 12:
                    monthstring = "dec";
                    break;
            }
            if (daystring.Length == 1)
            {
                daystring = "0" + day;
            }
            return monthstring + "-" + daystring + "-" + "news";
        }

    }
    public class Update
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }
}

