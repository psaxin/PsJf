using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectPsJf
{
    public class ConvertRss
    {
        private static XDocument xDoc = new XDocument();
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
        
    }
}
