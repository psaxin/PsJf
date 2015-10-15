using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Data;
using System.ComponentModel;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.IO;

using System.Drawing;

namespace ProjectPsJf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        // denna kodrad kommer behövas senare
        //private ObservableCollection<listViewItems> minLista = new ObservableCollection<listViewItems>();
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private XDocument xDoc = new XDocument();
        private string currentFile ="";
        public object MathHelper { get; private set; }
        private List<listViewItems> xmlList;
        DispatcherTimer timer = new DispatcherTimer();
       //private Timer aTimer;

        public MainWindow()
        {
            InitializeComponent();
            //skapar columner till listViewDetails. Detta kan vi kan skapa direkt i designern..
            UpdateSavedFeeds updateThread = new UpdateSavedFeeds();
            showSavedFeeds();
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            fyllLista();
        }

        public void fyllLista() {
            //hämtar text från textBox
            string rssUrl = textBox.Text;
            listViewDetails.Items.Clear();
            try
            {
                List<listViewItems> items = HanteraRss.toXml(rssUrl);
                
                foreach (var i in items)
                {
                    this.listViewDetails.Items.Add(i);
                  
                }
               
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("URL fungerade ej");

            }
            
        }

        // En eventlistener för att göra "realtids" validering av textBoxen för "URL"
        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            //testar validator klassen
            if (textBox.Text.isEmpty())
            {
                button.IsEnabled = false;
            }
            else {
                button.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            saveWindow saveWin = new saveWindow(textBox.Text, this);
            saveWin.Show();


        }
     
       private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            
            playMedia((listViewDetails.SelectedItem as listViewItems).URL);
         
              
        }
      

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // , mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss")
            if (mediaPlayer.Source != null)
                lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), "Under konstruktion...");
            else
                lblStatus.Content = "No file selected...";
        }

        private void listViewDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string chosenFile = (listViewDetails.SelectedItem as listViewItems).URL;
            playMedia(chosenFile);
          
        }

        private void playMedia(string file) {
            
            if (file != currentFile) 
            {
                mediaPlayer.Open(new Uri(file));
                mediaPlayer.Play();
                currentFile = file;
                
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            

            else { 
            mediaPlayer.Play();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            }
        }

        public void addToListBox(saveWindow save)
        {
             String feed = "";
             feed += save.tbNamn.Text;
             feed += save.tbKat.Text;
             feed += save.tbUppd.Text;
             //XDocument saveDoc = new XDocument();
             //saveDoc = XDocument.Load(feed);


            lwFeed.Items.Add(new listViewItems { Namn = save.tbNamn.Text, Kategori = save.tbKat.Text, Frekvens = save.tbUppd.Text });

            //xDoc.Save(@"savedFeeds/src/" + save.tbNamn.Text + ".xml");
            saveFeed(save.tbNamn.Text, save.tbKat.Text, save.tbUppd.Text);
            //saveDoc.Save(@"C:\Users\joaki_000\Desktop\C#\git\PsJf");


        }
        private void saveFeed(string name, string kat, string frek) {

            string path = @"savedFeeds/" + name + ".xml";
            createSaveFile.create(name,path,kat,frek);
            xDoc = XDocument.Load(textBox.Text);
            xDoc.Save(@"savedFeeds/src/" + name + ".xml");
            //Console.WriteLine((listViewDetails.SelectedItem as listViewItems).URL);
        }

        private void updateFeed(string chosenFile) {

            try
            {
                
                xDoc = XDocument.Load(chosenFile);
                //hämtar ut element från xDoc till en lista av objekt
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 // hämtar ut title element ur xdoc och ger objektet med namn title det värdet.

                                 title = x.Element("title").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 url = (string)x.Element("enclosure").Attribute("url").Value,
                             });
                //så länge items inte är tom..
                if (items != null)
                {
                    //tömmer listboxen
                    listViewDetails.Items.Clear();
                    //loopar igenom alla objekt i items.
                    foreach (var i in items)
                    {
                        //lägger till i listen
                        //Console.WriteLine(i.url);
                        this.listViewDetails.Items.Add(new listViewItems { Title = i.title, Date = i.pubDate, URL = i.url });
                        //listViewDetails.Items.Add.(i.title);
                    }
                }

            }

            catch (System.Net.WebException)
            {
                MessageBox.Show("URL fungerade ej");

            }


        }

    
       
        private void showSavedFeeds() {

            string[] filePaths = Directory.GetFiles(@"savedFeeds\");
            lwFeed.Items.Clear();
            foreach (string element in filePaths) {
                try
                {
                    xDoc = XDocument.Load(element);
                    
                    var items = (from x in xDoc.Descendants("body")
                                 select new
                                 {
                                     name = x.Element("Name").Value,
                                     path = x.Element("Path").Value,
                                     kat = x.Element("Kat").Value,
                                     frek = x.Element("Frek").Value
                                 });
                    
                    if (items != null)
                    {
                        foreach (var i in items)
                        {
                            lwFeed.Items.Add(new listViewItems { Namn = i.name, Kategori = i.kat, Frekvens = i.frek  });
                       
                        }
                    }

                }

                catch (System.Net.WebException)
                {
                    MessageBox.Show("URL fungerade ej");

                }
                //lwFeed.Items.Add(new listViewItems { Namn = save.tbNamn.Text, Kategori = save.tbKat.Text, Frekvens = save.tbUppd.Text });
            }
        }

        private void slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            float newVolume = (float)(Math.Sqrt(slider.Value) / 10);
            mediaPlayer.Volume = newVolume;

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string path ="";
            string pathSrc="";
            try
            {
                string chosenFile = (lwFeed.SelectedItem as listViewItems).Namn;
                path = @"savedFeeds/" + chosenFile + ".XML";
                pathSrc = @"savedFeeds/src/" + chosenFile + ".xml";
            }
            catch {

                printStatusMessage("Välj en fil att radera");
            }
           
            
            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("filen finns!!!");
                    File.Delete(path);
                    File.Delete(pathSrc);
                    printStatusMessage("Raderade filen: "+ path);
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
            showSavedFeeds();

        }

        private void listViewDetails_GotMouseCapture(object sender, MouseEventArgs e)
        {
           
            btnPlay.IsEnabled = true;
            //chosenFile = (listViewDetails.SelectedItem as listViewItems).URL;
           
        }

        private void listViewDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
            btnPlay.IsEnabled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
  
        }

        private void lwFeed_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            string chosenFile = (lwFeed.SelectedItem as listViewItems).Namn;
            updateFeed(@"savedFeeds/src/" + chosenFile + ".xml");

        }

        public void printStatusMessage(string message){
            lbStatusMessages.Items.Add("<"+DateTime.Now.ToShortTimeString() +"> "+ message);
        }
        }
    }
