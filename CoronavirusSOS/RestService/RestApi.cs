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
                    foreach(var nodehtml in HtmlNode.ChildNodes)
                    {
                        if (nodehtml.Name == "body")
                        {
                            foreach(var n in nodehtml.ChildNodes)
                            {
                                if (n.Name == "div")
                                {
                                    foreach(var i in n.ChildNodes)
                                    {
                                        if (i.Name == "div")
                                        {
                                            foreach (var f in n.ChildNodes)
                                            {
                                                if (f.Name == "div")
                                                {
                                                    foreach (var e in f.ChildNodes)
                                                    {
                                                        if(e.Id== "maincounter-wrap")
                                                        {
                                                            foreach(var g in e.ChildNodes)
                                                            {
                                                                if (g.Name == "div")
                                                                {
                                                                    foreach (var h in g.ChildNodes)
                                                                    {
                                                                        if (h.Name == "span"&&count>0)
                                                                        {
                                                                            if (count == 3)
                                                                            {
                                                                                values.Add(h.InnerText.ToString());
                                                                            }
                                                                            if (count == 2)
                                                                            {
                                                                                values.Add( h.InnerText.ToString());
                                                                            }
                                                                            if (count == 1)
                                                                            {
                                                                                values.Add( h.InnerText.ToString());
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
    }
}
