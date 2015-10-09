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

namespace ProjectPsJf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
        public MainWindow()
        {
            InitializeComponent();
            //skapar columner till listViewDetails. Detta kanske vi kan skapa direkt i designern..
            initializeGrid();
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
                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(rssUrl);
                //hämtar ut element från xDoc till en lista av objekt
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 // hämtar ut title element ur xdoc och ger objektet med namn title det värdet.
                               
                                     title = x.Element("title").Value,
                                     pubDate = x.Element("pubDate").Value,
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
                        this.listViewDetails.Items.Add(new listViewItems { Title = i.title , Date = i.pubDate });          
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

        //DENNA KOD KANSKE BEHÖVS SENARE
        //private void initializeGrid() {
        //    GridView myGridView = new GridView();
        //    myGridView.AllowsColumnReorder = true;
        //    myGridView.ColumnHeaderToolTip = "Employee Information";
        //    GridViewColumn gvc1 = new GridViewColumn();
        //    gvc1.DisplayMemberBinding = new Binding("Avsnitt");
        //    gvc1.Header = "Avsnitt";
        //    gvc1.Width = 100;
        //    myGridView.Columns.Add(gvc1);
        //    GridViewColumn gvc2 = new GridViewColumn();
        //    gvc2.DisplayMemberBinding = new Binding("Titel");
        //    gvc2.Header = "Titel";
        //    gvc2.Width = 100;
        //    myGridView.Columns.Add(gvc2);
        //    GridViewColumn gvc3 = new GridViewColumn();
        //    gvc3.DisplayMemberBinding = new Binding("Övrigt");
        //    gvc3.Header = "Övrigt";
        //    gvc3.Width = 100;
        //    myGridView.Columns.Add(gvc3);
        //    //myGridView.SetValue(gvc1, "dd");
        //    listViewDetails.View = myGridView;
        //}

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

    }
    }
