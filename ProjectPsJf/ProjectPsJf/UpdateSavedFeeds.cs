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
                 temp = HanteraRss.ParseToString("http://alexosigge.libsyn.com/rss");
            try
            {
                    fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs);
                    writer.Write(temp);
                    writer.Flush();
                    writer.Close();
                    fs.Close();
                Console.WriteLine("skrev" + filepath);
            }
            catch (FileNotFoundException x)
            {

                Console.WriteLine("misslyckades läsa " + filepath);

            }
            catch (IOException x)
            {

                Console.WriteLine("misslyckades läsa " + filepath);

            }
            catch (Exception ege)
            {

                Console.WriteLine("misslyckades läsa  " + filepath);

            }
            //finally
            //{
            //    if (fs != null)
            //        fs.Close();
            //}

            
           

        }


       
    }

       
    }




