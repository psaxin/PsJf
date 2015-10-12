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

        public object MathHelper { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            //skapar columner till listViewDetails. Detta kan vi kan skapa direkt i designern..
            initializeGrid();
            updateSavedFeeds();
            
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            fyllLista();
        }

        public void fyllLista() {
            //hämtar text från textBox
            string rssUrl = textBox.Text;

            try
            {
                
                xDoc = XDocument.Load(rssUrl);
                //hämtar ut element från xDoc till en lista av objekt
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 // hämtar ut title element ur xdoc och ger objektet med namn title det värdet.
                               
                                     title = x.Element("title").Value,
                                     pubDate = x.Element("pubDate").Value,
                                     url = (string) x.Element("enclosure").Attribute("url").Value,
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
                        this.listViewDetails.Items.Add(new listViewItems { Title = i.title , Date = i.pubDate, URL = i.url });          
                        //listViewDetails.Items.Add.(i.title);
                    }
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
            // denna ska byggas om. Direkt i XAML istället
        private void initializeGrid()
        {
            var gridview = new GridView();
            this.listViewDetails.View = gridview;
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Title",
                DisplayMemberBinding = new Binding("Title"),
                Width = 150,
            });
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Date",
                DisplayMemberBinding = new Binding("Date")
            });

            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            saveWindow saveWin = new saveWindow(textBox.Text, this);
            saveWin.Show();


        }
     
       private void btnPlay_Click(object sender, RoutedEventArgs e)
        {/*
            string chosenFile = (listViewDetails.SelectedItem as listViewItems).URL;
            if (mediaPlayer.Source.ToString() == chosenFile)
            {
                mediaPlayer.Play();
                
            }
            else {
                playMedia(chosenFile);
            }
              * */
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
            mediaPlayer.Open(new Uri(file));
            mediaPlayer.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
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
            xDoc.Save(@"savedFeeds/src/" + name + ".xml");
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

        private void lwFeed_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string chosenFile = (lwFeed.SelectedItem as listViewItems).Namn;
            Console.WriteLine(@"savedFeeds/src/" + chosenFile + ".xml");
            updateFeed(@"savedFeeds/src/" +chosenFile +".xml");
            
        }

        private void btnRemoveSaved_Click(object sender, RoutedEventArgs e)
        {
            
            string chosenFile = (lwFeed.SelectedItem as listViewItems).Namn;
            string path = @"savedFeeds/" + chosenFile + ".XML";
            string pathSrc = @"savedFeeds/src/" + chosenFile + ".xml";
            Console.WriteLine(path);
            Console.WriteLine(pathSrc);
            try {
                if (File.Exists(path))
                {
                    Console.WriteLine("filen finns!!!");
                    File.Delete(path);
                    File.Delete(pathSrc);
                }
                else
                {
                    Console.WriteLine("filen finns ej");
                }
            }
            catch (Exception re){

                Console.WriteLine(re);
            }
            updateSavedFeeds();

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float newVolume = (float)(Math.Sqrt(slider.Value) / 10);
            mediaPlayer.Volume = newVolume;
        }

        private void updateSavedFeeds() {

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
    }
}
