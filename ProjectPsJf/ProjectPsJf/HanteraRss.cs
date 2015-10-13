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
            xDoc = XDocument.Load(url);
            //hämtar ut element från xDoc till en lista av objekt
            var items = (from x in xDoc.Descendants("item")
                         select new
                         {
                             // hämtar ut title element ur xdoc och ger objektet med namn title det värdet.

                             title = x.Element("title").Value,
                             pubDate = x.Element("pubDate").Value,
                             url = (string)x.Element("enclosure").Attribute("url").Value,
                         });

            if (items != null)
            {
                
                foreach (var i in items)
                {
                    xmlList.Add(new listViewItems{ Title = i.title, Date = i.pubDate, URL = i.url });

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

        
    }
}
