using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


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
            string[] frekPath = Directory.GetFiles(@"savedFeeds\");

            foreach (var element in filePaths) {
                url = HanteraRss.getURL(element);
                filepath = element;
                name = getName(element);
                foreach (var frek in frekPath)
                {
                    frekvens = HanteraRss.getFrek(frek);
                    Console.WriteLine(name + "\n" + url + "\n" + Int32.Parse(frekvens));
                }
                //setUpdate(name, url, Int32.Parse(frekvens));
                
            }

        
        }

        private void setUpdate(string namn, string url, int frekvens) {
 

            var Timer = new System.Threading.Timer(startUpdate, null, 0, frekvens);

        }

        private void startUpdate(object x) {

           
            // Don't do anything if the form's handle hasn't been created 
            // or the form has been disposed.
            if (!this.IsHandleCreated && !this.IsDisposed) return;
            // Invoke an anonymous method on the thread of the form.
            this.Invoke((MethodInvoker)delegate
            {
                tempXDoc = XDocument.Load(filepath);
                tempXDoc.Save(@"savedFeeds/src/" + name + ".xml");

        });
        }

        private string getName(string path) {
            int pos = path.LastIndexOf("\\") +1;
            string newString = path.Substring(pos);
            return newString;

        }


    }



}
