using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace GUI
{
    public class HanteraRss
    {
        private static XDocument xDoc = new XDocument();
        private static XDocument xDocPath = new XDocument();
        private static List<listViewItems> xmlList = new List<listViewItems>();
        private static XmlDocument rssXmlDoc = new XmlDocument();
        


        public static List<listViewItems> toXml(string url)
        {
            if(validate.IsValidFeedUrl(url))
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

                    xmlList.Add(new listViewItems { Title = i.title, Date = i.pubDate, URL = i.url, Played = false, Stamp = "none" });

                }

            }
            return xmlList;
            }

            else
            {
                return xmlList;
            }

        }


        public static string getURL(string path)
        {
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

        //Lägger till en xml taggen <ID> i <Played> om den inte finns xml dokumentet
        // parametern "path" är sökvägen för xml filen och "playedID" är värdet för <ID> som den vill lägga till
        public static void addPlayed(string path, string playedID)
        {

            bool exist = checkPlayedExist(getPlayed(path), playedID);
            if (exist == false)
            {
                rssXmlDoc.Load(path);
                XmlNode played = rssXmlDoc.DocumentElement.LastChild;
                XmlElement ID = rssXmlDoc.CreateElement("ID");
                ID.InnerText = playedID;
                played.AppendChild(ID);
                rssXmlDoc.Save(path);
                Console.WriteLine("Första gången spelad");
            }
            else { Console.WriteLine("Redan spelad"); }

        }


        //Denna metod gör en Lista med alla <ID> från xml dokumentet som tas i emot i parametern, dvs fileName
        // ID representerar alltså de spelade filerna
        public static List<string> getPlayed(string fileName)
        {

            rssXmlDoc.Load(fileName);
            XmlNodeList playedIdNodes = rssXmlDoc.SelectNodes("body/Played/ID");
            List<string> playedList = new List<string>();

            foreach (XmlNode ID in playedIdNodes)
            {
                playedList.Add(ID.InnerText);
            }
            return playedList;

        }


        //Denna metod tar emot en lista som den ska söka igenom och en string som är värdet den ska leta efter
        //returnar true om "itemToCheck" finns i listan "played", annars false.
        public static bool checkPlayedExist(List<string> played, string itemToCheck)
        {

            foreach (string ID in played)
            {

                if (ID == itemToCheck)
                {

                    return true;
                }
            }

            return false;

        }



    }
}

