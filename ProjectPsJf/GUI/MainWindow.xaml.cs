﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using System.Data;
using System.ComponentModel;
using System.Windows.Threading;
using System.IO;
using Logic;
using Data;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private XDocument xDoc = new XDocument();
        private string currentFile = "";
        public object MathHelper { get; private set; }
        private List<ListItems> xmlList;
        DispatcherTimer timer = new DispatcherTimer();
        List<ListItems> items;
        public Boolean asc;

        public MainWindow()
        {
            InitializeComponent();
            UpdateSavedFeeds updateThread = new UpdateSavedFeeds(this);
            showSavedFeeds();
            asc = false;

        }

        // Denna metod fyller en lista med items från en xml-fil som konverteras från en rss-url genom HanterRss.
        private void fillListFromUrl()
        {
            string rssUrl = tbUrl.Text;
            listViewDetails.Items.Clear();
            items = HanteraRss.toXml(rssUrl);
            foreach (var i in items)
            {
                this.listViewDetails.Items.Add(i);
            }
        }

        // Öppnar ett nytt fönster som används för att spara en profil
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            saveWindow saveWin = new saveWindow(tbUrl.Text, this);
            saveWin.Show();


        }
        // Startar en ljudfil från en url i list Objekten
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {


            playMedia((listViewDetails.SelectedItem as ListItems).URL);


        }
        // Pausar den nuvarande spelningen.
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

            mediaPlayer.Stop();
        }
        // Stoppar den 
        void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null)
                lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"hh\:mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
            else
                lblStatus.Content = "No file selected...";
        }
        private void listViewDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listViewDetails.SelectedItem != null){
                string chosenItemOwner = (listViewDetails.SelectedItem as ListItems).Stamp;
                string chosenItemUrl = (listViewDetails.SelectedItem as ListItems).URL;
                string itemTitle = (listViewDetails.SelectedItem as ListItems).Title;
                if (chosenItemOwner != "none")
                {
                    HanteraRss.addPlayed(@"SavedFeeds\" + chosenItemOwner + ".xml", itemTitle);
                    setPlayed(@"savedFeeds/src/" + chosenItemOwner + ".xml", chosenItemOwner);
                }
                playMedia(chosenItemUrl);
                lblFileName.Content = itemTitle;
            }
        }
        private void playMedia(string file)
        {
            if (file != currentFile)
            {
                mediaPlayer.Open(new Uri(file));
                mediaPlayer.Play();
                currentFile = file;

                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }           
            else
            {
                mediaPlayer.Play();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
        }
        public void addToListBox(saveWindow save)
        {

            if (validate.notNullOrEmpty(save.tbNamn.Text) && validate.notNullOrEmpty(save.tbKat.Text) && validate.notNullOrEmpty(save.tbNamn.Text) &&
                validate.isLetters(save.tbNamn.Text) && validate.isLetters(save.tbKat.Text) && validate.isDigit(save.tbUppd.Text)
                )
            {
                String feed = "";
                feed += save.tbNamn.Text;
                feed += save.tbKat.Text;
                feed += save.tbUppd.Text;
                lwSavedFeeds.Items.Add(new SavedItems { Namn = save.tbNamn.Text, Kategori = save.tbKat.Text, Frekvens = save.tbUppd.Text, Stamp = save.tbNamn.Text });

                Profile saveProfile = new Profile();
                Feed saveFeed = new Feed();
                saveProfile.save(save.tbNamn.Text, tbUrl.Text, save.tbKat.Text, save.tbUppd.Text);
                saveFeed.save(save.tbNamn.Text, tbUrl.Text, save.tbKat.Text, save.tbUppd.Text);
            }

            else
            {
                printStatusMessage("Vänliga fyll i alla fält med efterfrågad data");
            }

        }
        private void updateFeed(string chosenFile, string fileStamp)
        {
            List<string> played = new List<string>();

            played = HanteraRss.getPlayed(@"savedFeeds/" + fileStamp + ".xml");
            //körs om den inte är tom, detta är för att GetItemAt nedanför inte ska vara null och ge error.
            if ((listViewDetails.Items.IsEmpty) == false)
            {
                //körs om den har en en annorlunda stamp
                if ((listViewDetails.Items.GetItemAt(0) as ListItems).Stamp != fileStamp)
                {
                    fillFeedList(chosenFile,fileStamp,played);
                }
            }
            // körs den är tom
            else if (listViewDetails.Items.Count == 0)
            {
                fillFeedList(chosenFile, fileStamp, played);
            }

            // denna kommer gå igenom om listan inte är tom och Stamp är lika. Det betyder att listan bara behöver uppdateras.
            // tex om en låt är spelad och behöver få sin nya färg.
            else
            {

                listViewDetails.Items.Clear();

                foreach (var i in items)
                {
                    foreach (var x in played)
                    {
                        if (i.Title == x)
                        {
                            i.Played = true;
                        }
                    }

                    this.listViewDetails.Items.Add(i);
                }

            }

        }
        private void fillFeedList(string chosenFile, string fileStamp, List<string> playlist) {

            try
            {
                listViewDetails.Items.Clear();
                xDoc = XDocument.Load(chosenFile);
                //hämtar ut element från xDoc till en lista av objekt
                var newItems = (from x in xDoc.Descendants("item")
                                select new
                                {
                                    // hämtar ut title element ur xdoc och ger objektet med namn title det värdet.
                                    title = x.Element("title").Value,
                                    pubDate = x.Element("pubDate").Value,
                                    url = (string)x.Element("enclosure").Attribute("url").Value,
                                });
                //så länge items inte är tom..
                if (newItems != null)
                {
                    listViewDetails.Items.Clear();
                    foreach (var i in newItems)
                    {
                        bool played = false;
                        if (HanteraRss.checkPlayedExist(playlist, i.title))
                        {
                            played = true;
                        }
                        this.listViewDetails.Items.Add(new FeedItems { Title = i.title, Date = i.pubDate, URL = i.url, Played = played, Stamp = fileStamp });
                    }
                }
            }

            catch (System.Net.WebException)
            {
                MessageBox.Show("URL fungerade ej");

            }

        }
        private void showSavedFeeds()
        {

            string[] filePaths = Directory.GetFiles(@"savedFeeds\");
            lwSavedFeeds.Items.Clear();
            foreach (string element in filePaths)
            {
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
                            lwSavedFeeds.Items.Add(new SavedItems { Namn = i.name, Kategori = i.kat, Frekvens = i.frek, Stamp = i.name });

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
            string path = "";
            string pathSrc = "";
            try
            {
                string chosenFile = (lwSavedFeeds.SelectedItem as ListItems).Namn;
                path = @"savedFeeds/" + chosenFile + ".XML";
                pathSrc = @"savedFeeds/src/" + chosenFile + ".xml";
            }
            catch
            {

                printStatusMessage("Välj en fil att radera");
            }


            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("filen finns!!!");
                    File.Delete(path);
                    File.Delete(pathSrc);
                    printStatusMessage("Raderade filen: " + path);
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
        private void lwFeed_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
          
            if (lwSavedFeeds.SelectedItem != null) {
                string chosenFile = (lwSavedFeeds.SelectedItem as ListItems).Namn;
                string fileStamp = (lwSavedFeeds.SelectedItem as ListItems).Stamp;
                updateFeed(@"savedFeeds/src/" + chosenFile + ".xml", fileStamp);
            }

        }
        public void printStatusMessage(string message)
        {

            lbStatusMessages.Items.Add("<" + DateTime.Now.ToShortTimeString() + "> " + message);


        }
        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
        {


            if (asc == true)
            {
                descending();
                return;
            }
            else
            {
                lwSavedFeeds.Items.SortDescriptions.Clear();
                lwSavedFeeds.Items.SortDescriptions.Add(new SortDescription("Kategori", ListSortDirection.Ascending));
                lwSavedFeeds.Items.SortDescriptions.Add(new SortDescription("Namn", ListSortDirection.Ascending));
                lwSavedFeeds.Items.SortDescriptions.Add(new SortDescription("Frekvens", ListSortDirection.Ascending));
                asc = true;
            }

        }
        private void descending()
        {

            lwSavedFeeds.Items.SortDescriptions.Clear();
            lwSavedFeeds.Items.SortDescriptions.Add(new SortDescription("Kategori", ListSortDirection.Descending));
            lwSavedFeeds.Items.SortDescriptions.Add(new SortDescription("Namn", ListSortDirection.Descending));
            lwSavedFeeds.Items.SortDescriptions.Add(new SortDescription("Frekvens", ListSortDirection.Descending));

            asc = false;
        }
        private void setPlayed(string chosenFile, string fileStamp)
        {
            List<string> playlist = new List<string>();
            playlist = HanteraRss.getPlayed(@"savedFeeds/" + fileStamp + ".xml");
            try
            {
                listViewDetails.Items.Clear();
                xDoc = XDocument.Load(chosenFile);
                var newItems = (from x in xDoc.Descendants("item")
                                select new
                                {
                                    title = x.Element("title").Value,
                                    pubDate = x.Element("pubDate").Value,
                                    url = (string)x.Element("enclosure").Attribute("url").Value,
                                });

                if (newItems != null)
                {
                    listViewDetails.Items.Clear();
                    foreach (var i in newItems)
                    {
                        bool played = false;
                        if (HanteraRss.checkPlayedExist(playlist, i.title))
                        {
                            played = true;
                        }
                        this.listViewDetails.Items.Add(new FeedItems { Title = i.title, Date = i.pubDate, URL = i.url, Stamp = fileStamp, Played = played });
                    }
                }

            }

            catch (System.Net.WebException)
            {
                MessageBox.Show("URL fungerade ej");

            }


        }
        public void redigera(saveWindow save)
        {
            XDocument xDocEdit = new XDocument();
            string chosenFile = (lwSavedFeeds.SelectedItem as ListItems).Namn;
            string path = @"savedFeeds/" + chosenFile + ".XML";
            Console.WriteLine(path);
            xDocEdit = XDocument.Load(path);
            xDocEdit.Root.Element("Kat").Value = save.tbKat.Text;
            xDocEdit.Save(path);
            showSavedFeeds();
        }
        private void btn_Redigera_Click_1(object sender, RoutedEventArgs e)
        {

            saveWindow saveWin = new saveWindow(tbUrl.Text, this);
            saveWin.Show();
            saveWin.tbNamn.Enabled = false;
            saveWin.tbUppd.Enabled = false;

        }
        private void lwFeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            btn_Redigera.IsEnabled = true;

        }
        private void btn_getUrl_Click(object sender, RoutedEventArgs e)
        {
            fillListFromUrl();
        }
        // En eventlistener för att göra "realtids" validering av textBoxen för "URL"
        private void tbUrl_KeyUp(object sender, KeyEventArgs e)
        {
            //testar validator klassen
            if (tbUrl.Text.isEmpty())
            {
                btn_getUrl.IsEnabled = false;
            }
            else
            {
                btn_getUrl.IsEnabled = true;
            }
        }
    }
}




