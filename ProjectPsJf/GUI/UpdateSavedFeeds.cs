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

namespace GUI
{


   public class UpdateSavedFeeds : Form
    {
        private MainWindow mainForm;
        private int itteration = 0;
        private int time = 0;
        private string url;
        private string name;
        private string frekvens;
        private string filepath;
        private static string[,] minTabell;
        private XDocument tempXDoc = new XDocument();

        public UpdateSavedFeeds(MainWindow main)
        {
            mainForm = main;
            initialize();
        }

        private void initialize()
        {
            setUpTable();
            startUpdateThread();
        }
        private void setUpTable()
        {

            minTabell = new string[9, 3];

            string[] filePaths = Directory.GetFiles(@"savedFeeds\");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j != 3; j++)
                {
                    minTabell[i, 2] = "0";
                }

            }
        }

        private void startUpdateThread()
        {
            var timer = new System.Timers.Timer(60000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
        }

        private string getName(string path)
        {
            int pos = path.LastIndexOf("\\") + 1;
            string newString = path.Substring(pos);
            return newString;
        }


        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {


            itteration++;
            collectSavedFiles();
            collectFrek();
            addCurrentToAll();

            string[] filePaths = Directory.GetFiles(@"savedFeeds\src\");
            Console.WriteLine("Påbörjar " + itteration);
            string temp;
            FileStream fs;
            int theTime = time;
            for (int i = 0; i < filePaths.Length; i++)
            {


                filepath = minTabell[i, 0];
                url = HanteraRss.getURL(filepath);
                temp = HanteraRss.ParseToString(url);
                //name = getName(element);
                //frekvens = HanteraRss.getFrek(@"savedFeeds\" + getName(element));
                if (Int32.Parse(minTabell[i, 1]) == Int32.Parse(minTabell[i, 2]))
                {
                    Console.WriteLine("Ifen gick igenom på  " + itteration);
                    try
                    {
                        fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                        StreamWriter writer = new StreamWriter(fs);
                        writer.Write(temp);
                        writer.Flush();
                        writer.Close();
                        fs.Close();

                        mainForm.Dispatcher.BeginInvoke(new Action(delegate()
                        {
                            //fs.getName är hela filepath, så vi kallar på metoden getName för att trimma strängen till filnamnet
                            mainForm.printStatusMessage("Uppdaterade " + getName(fs.Name));
                        }));
                    }
                    catch (FileNotFoundException x)
                    {

                        Console.WriteLine("FileNotFoundException");

                    }
                    catch (IOException x)
                    {

                        Console.WriteLine("IOException");

                    }
                    catch (Exception ege)
                    {

                        Console.WriteLine("Exception");

                    }
                    //otroligt viktig kodrad.  :)
                    minTabell[i, 2] = "0";
                }


            }

        }

        private void addCurrentToAll()
        {

            string[] filePaths = Directory.GetFiles(@"savedFeeds\");
            for (int i = 0; i < 9; i++)
            {
                int temp = Int32.Parse(minTabell[i, 2]);
                temp++;
                minTabell[i, 2] = temp.ToString();
            }

        }
        private void collectSavedFiles()
        {

            string[] filePaths = Directory.GetFiles(@"savedFeeds\src\");

            for (int i = 0; i < filePaths.Length; i++)
            {
                minTabell[i, 0] = filePaths[i];
            }

        }
        private void collectFrek()
        {
            string[] filePaths = Directory.GetFiles(@"savedFeeds\");
            for (int i = 0; i < filePaths.Length; i++)
            {
                minTabell[i, 1] = HanteraRss.getFrek(filePaths[i]);

            }

        }



    }
}




