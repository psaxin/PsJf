using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace ProjectPsJf
{
    public class HanteraRss
    {
        private static XDocument xDoc = new XDocument();
        private static XDocument xDocPath = new XDocument();
        private static List<listViewItems> xmlList = new List<listViewItems>();

        public static List<listViewItems> toXml(string url)
        {
            xmlList.Clear();
            xDoc = XDocument.Load(url);
            //hämtar ut element från xDoc till en lista av objekt
            var newitems = (from x in xDoc.Descendants("item")
                         select new
                         {
                             // hämtar ut title element ur xdoc och ger objektet med namn title det värdet.

                             title = x.Element("title").Value,
                             pubDate = x.Element("pubDate").Value,
                             url = (string)x.Element("enclosure").Attribute("url").Value,
                         });

            if (newitems != null)
            {
                
                foreach (var i in newitems)
                {
                    xmlList.Add(new listViewItems{ Title = i.title, Date = i.pubDate, URL = i.url, Seen = false, Stamp = i.url});

                }
               
            }

            return xmlList;
      
        }


        public static string getURL(string path) {
            xDocPath = XDocument.Load(path);
            XNamespace atom = "http://www.w3.org/2005/Atom";
            path = xDocPath.Root.Element("channel").Element(atom + "link").Attribute("href").Value;
            return path;
        }

        public static string getFrek(string path)
        {
            string frek;
            xDocPath = XDocument.Load(path);
            //Console.WriteLine(path);
            frek = xDocPath.Root.Element("Frek").Value;
            return frek;
        }

        public static string ParseToString(string urlin)
        {
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load(urlin);

            // Parse the Items in the RSS file
            //XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss");

            StringBuilder rssContent = new StringBuilder();


            // Iterate through the items in the RSS file
            rssContent.Append("<?xml version='1.0' encoding='UTF-8'?>");
            rssContent.Append("<rss version='2.0' xmlns:atom='http://www.w3.org/2005/Atom'>");
            rssContent.Append("<channel>");
            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("channel");
                string channel = rssSubNode != null ? rssSubNode.InnerXml : "";

                //XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                //string title = rssSubNode != null ? rssSubNode.InnerText : "";

                //rssSubNode = rssNode.SelectSingleNode("link");
                //string link = rssSubNode != null ? rssSubNode.InnerText : "";

                //rssSubNode = rssNode.SelectSingleNode("description");
                //string description = rssSubNode != null ? rssSubNode.InnerText : "";


                //rssSubNode = rssNode.SelectSingleNode("//enclosure/@url");
                //string urlout = rssSubNode != null ? rssSubNode.InnerText : "";
                ////(string)x.Element("enclosure").Attribute("url").Value,
                //rssContent.Append("<item><title>"+ title + "</title><enclosure>"+ urlout +"</enclosure></item>");
                rssContent.Append(channel);
            }
            rssContent.Append("</channel>");
            rssContent.Append("</rss>");
            // Return the string that contain the RSS items
            return rssContent.ToString();
        }


    }
}
