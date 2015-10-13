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
            
            foreach (var element in filePaths) {
                url = HanteraRss.getURL(element);
                filepath = element;
                name = getName(element);
                frekvens = HanteraRss.getFrek(@"savedFeeds\" + getName(element));
                setUpdate(name, url, Int32.Parse(frekvens) * 1000);
                //Console.WriteLine("Foreachen loopens värde" + name  + url  + Int32.Parse(frekvens) * 10000);
                
                
              
                
            }

        
        }

        private void setUpdate(string namn, string url, int frekvens) {

            var timer = new System.Timers.Timer(frekvens);
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

            save();
            Console.WriteLine("Timern körs", e.SignalTime);
        }

        private void save()
        {
           System.Security.Permissions.FileIOPermissionAccess.Write
            using (var fi = new System.IO.FileStream(filepath, System.IO.FileMode.Create))
            {

                using (var bw = new System.IO.BinaryWriter(fi, System.Text.Encoding.UTF8))
                {
                       tempXDoc = XDocument.Load(filepath);
                       tempXDoc.Save(@"savedFeeds/src/" + name);
                       bw.Flush();
                       bw.Close();
                }



            }
               
            }
            
              
        }

       
    }




