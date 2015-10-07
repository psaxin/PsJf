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
                             });
                //så länge items inte är tom..
                if (items != null)
                {
                    //tömmer listboxen
                    listBox1.Items.Clear();
                    //loopar igenom alla objekt (alltså "i") i items.
                    foreach (var i in items)
                    {
                        //lägger till i listboxen
                        listBox1.Items.Add(i.title);
                    }
                }

            }
       
            catch (System.Net.WebException)
            {
                MessageBox.Show("URL fungerade ej");
                
            }
        }


    }
    }
