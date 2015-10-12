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
        private static Array xmlArray;

        public static Array toXml(string url)
        {

            int antal = 0;
            int plats = 0;

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
                //tömmer listboxen

                //loopar igenom alla objekt i items.

                foreach (var i in items)
                {

                    antal++;


                }

                xmlArray = new listViewItems[antal];

                foreach (var i in items)
                {
                    
                    //lägger till i listen
                    //Console.WriteLine(i.url);
                    xmlArray.SetValue((new listViewItems { Title = i.title, Date = i.pubDate, URL = i.url }), plats);
                    //listViewDetails.Items.Add.(i.title);
                    plats++;
                }
               
            }

            return xmlArray;
          
        }
        
    }
}
