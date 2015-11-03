using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
namespace Logic
{
    public class HanteraRss
    {
        private static XDocument xDoc = new XDocument();
        private static XDocument xDocPath = new XDocument();
        private static List<ListItems> xmlList = new List<ListItems>();
        private static XmlDocument rssXmlDoc = new XmlDocument();
        // Syftet är att skapa ett Xdoc ifrån urln i parameter listan.
        public static List<ListItems> toXml(string url)
        {
            if (validate.IsValidFeedUrl(url))
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

                        xmlList.Add(new FeedItems { Title = i.title, Date = i.pubDate, URL = i.url, Played = false, Stamp = "none" });

                    }

                }
                return xmlList;
            }

            else
            {
                return xmlList;
            }

        }
        // Syftet med funktion för att dra ut urln ifrån ett XDocument.
        public static string getURL(string path)
        {
            xDocPath = XDocument.Load(path);
            XNamespace atom = "http://www.w3.org/2005/Atom";
            path = xDocPath.Root.Element("channel").Element(atom + "link").Attribute("href").Value;
            return path;
        }
        // Syftet med nedan funktion är att dra ut frekvensen ifrån ett XDoc.
        public static string getFrek(string path)
        {
            string frek;
            xDocPath = XDocument.Load(path);
            //Console.WriteLine(path);
            frek = xDocPath.Root.Element("Frek").Value;
            return frek;
        }
        // Funktionens syfte är att omvandla ett rss dokument till en sträng.
        public static string ParseToString(string rssin)
        {
            // Load the RSS file from the RSS URL
            rssXmlDoc.Load(rssin);
            // Parse the Items in the RSS file
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
        // Denna metod gör en Lista med alla <ID> från xml dokumentet som tas i emot i parametern, dvs fileName
        // ID representerar alltså de spelade filerna
        public static List<string> getPlayed(string fileName)
        {
            List<string> playedList = new List<string>();
            if (validate.IsValidFeedUrl(fileName)) {
                rssXmlDoc.Load(fileName);
                XmlNodeList playedIdNodes = rssXmlDoc.SelectNodes("body/Played/ID");
                foreach (XmlNode ID in playedIdNodes)
                {
                    playedList.Add(ID.InnerText);
                }
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

        public static void addCategory(string newCategory) {

            XElement doc = XElement.Load(@"savedFeeds/cat/cat.xml");

            doc.Add(new XElement("category",new XAttribute("name", newCategory)));
            doc.Save(@"savedFeeds/cat/cat.xml");
        }
        public static void removeCategory(string category) {

            XElement doc = XElement.Load(@"savedFeeds/cat/cat.xml");

            IEnumerable<XElement> categoryDelete =
                from el in doc.Elements("category")
                where (string)el.Attribute("name") == category
                select el;
            foreach (XElement ele in categoryDelete)
            {
                categoryDelete.FirstOrDefault().Remove();
            }
            doc.Save(@"savedFeeds/cat/cat.xml");
        }

        public static void editCategory(string name, string newName) {


            XElement doc = XElement.Load(@"savedFeeds/cat/cat.xml");

            IEnumerable<XElement> categoryEdit =
                 from el in doc.Elements("category")
                 where (string)el.Attribute("name") == name
                 select el;

            categoryEdit.FirstOrDefault().SetAttributeValue("name", newName);

            doc.Save(@"savedFeeds/cat/cat.xml");

        }

        public static void removeFile(string path,string pathSrc)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    File.Delete(pathSrc);
                    
                }
                else
                {
                    Console.WriteLine("filen finns ej");
                }
            }
            catch (Exception re)
            {

                Console.WriteLine(re);
            }

        }


        public static void removeFeedCategory(string name) {


            string[] filePaths = Directory.GetFiles(@"savedFeeds\");

            foreach (var doc in filePaths) {
                
                XDocument xdoc = XDocument.Load(doc);

                
                    xdoc.Save(doc);
                
               
            }
           

        }

    }

         
}

