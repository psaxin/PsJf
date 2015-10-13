using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Timers;
using System.Security.Permissions;
using System.Xml;
using System.Net;

namespace ProjectPsJf
{

 
     class UpdateSavedFeeds : Form
    {
        private string url;
        private string name;
        private string frekvens;
        private string filepath;
        private XDocument tempXDoc = new XDocument();
        public UpdateSavedFeeds() {
            
            checkFeeds();
            //startUpdate();
        }

        private void checkFeeds() {
            //savedFeeds\src\alexosigge.xml
            string[] filePaths = Directory.GetFiles(@"savedFeeds\src\");
            string[] hehe = filePaths;

            filePaths = null;

            foreach (var element in hehe) {
               url = HanteraRss.getURL(element);
              filepath = element;
              name = getName(element);
                frekvens = HanteraRss.getFrek(@"savedFeeds\" + getName(element));
               setUpdate(name, url, Int32.Parse(frekvens) * 1000);

               //Console.WriteLine("Foreachen loopens värde" + name  + url  + Int32.Parse(frekvens) * 10000);


            }
         
        
        }

        private void setUpdate(string namn, string url, int frekvens) {
            // GLÖM INTE FREKVENSEN!!!!
            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;

        }

       
        

        private string getName(string path) {
            int pos = path.LastIndexOf("\\") +1;
            string newString = path.Substring(pos);
            return newString;

        }


        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
           

            //using (FileStream fs = new FileStream(filepath,
            //  FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            //{
            //    //XmlDocument newdoc = new XmlDocument();
            //    //newDoc.Load("http://alexosigge.libsyn.com/rss");

            //    //newdoc.Save(filepath);
            //}


            string temp;
            FileStream fs;
            fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);
            temp = ParseRssFile("http://alexosigge.libsyn.com/rss");
            writer.Write(temp);
            writer.Flush();
            Console.WriteLine("skrev");
           writer.Close();
            fs.Close();


        }


        private string ParseRssFile(string urlin)
        {
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load(urlin);

            // Parse the Items in the RSS file
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            StringBuilder rssContent = new StringBuilder();

            rssContent.Append("<channel>");
            // Iterate through the items in the RSS file
           
            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";


                rssSubNode = rssNode.SelectSingleNode("//enclosure/@url");
                string urlout = rssSubNode != null ? rssSubNode.InnerText : "";
                //(string)x.Element("enclosure").Attribute("url").Value,
                rssContent.Append("<item><title>"+ title + "</title><enclosure>"+ urlout +"</enclosure></item>");
            }
            rssContent.Append("/channel>");
            // Return the string that contain the RSS items
            return rssContent.ToString();
        }
    }

       
    }




