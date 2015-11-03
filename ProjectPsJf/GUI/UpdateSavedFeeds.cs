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
using Logic;
namespace GUI
{


   public class UpdateSavedFeeds : Form
    {
        private MainWindow mainForm;
        private int itteration = 0;
        private int time = 0;
        private string url;
        private string filepath;
        private static string[,] minTabell;
        private XDocument tempXDoc = new XDocument();
        // Konstruktor kallas ifrån MainWindows konstruktor, så körs direkt när exe filen körs.
        public UpdateSavedFeeds(MainWindow main)
        {
            mainForm = main;
            initialize();
        }
        // Bygger klassen
        private void initialize()
        {
            setUpTable();
            startUpdateThread();
        }
        // Fyller en tvådimensionell array med alla sparade profiler.
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
        // Startar en ny tråd som ska som kör funktionen onTimedEvent varje minut.
        private void startUpdateThread()
        {
            var timer = new System.Timers.Timer(6000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
        }
        // Trimmar en src till bara "filnamnet" t.ex savedFeeds\\src\\namn till namn.
        private string getName(string path)
        {
            int pos = path.LastIndexOf("\\") + 1;
            string newString = path.Substring(pos);
            return newString;
        }
        // Syftet med denna metod är att möjliggöra att en podcast hämtas efter användares angivna frekvens.
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
                            //fs.Name är hela filepath, så vi kallar på metoden getName för att trimma strängen till filnamnet
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
        // Syftet med metoden är att ge alla objekt i den statiska arrayn ett inkrement. Detta inkrement jämnförs mot det angivna frekvensen. Den ska alltså immitera ett tidtagare.
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
        // fyller på minTabell med sparade filer
        private void collectSavedFiles()
        {

            string[] filePaths = Directory.GetFiles(@"savedFeeds\src\");
            
            for (int i = 0; i < filePaths.Length; i++)
            {
                minTabell[i, 0] = filePaths[i];
            }

        }
        // fyller alla listor i minTabell med index 2, med en frekvens
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




