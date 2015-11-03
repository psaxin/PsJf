using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class saveWindow : Form
    {
        private MainWindow mainForm;
        private static string button;
        //  En overloaded konstruktor som först tar två parametrar sedan anropar default konstruktorn som bygger formen. Tar ett objekt av klassen MainWindow för att kunna använda dess metoder.
        public saveWindow(string url, string whichButton, MainWindow main): this()
        {
            button = whichButton;
            mainForm = main;
            tbUrl.Text = url;

        }
        //  Standard konstruktor som bygger klassen.
        public saveWindow()
        {

            InitializeComponent();

        }
        // Event på en knapp som möjliggör att man kan spara ner en xml fil. Anropar en spara funktion i mainWindow.
        private void btnSpara_Click(object sender, EventArgs e)
        {
            if (button == "edit")
            {
                mainForm.redigera(this);
                this.Dispose();
            }

            else
            {
                mainForm.addToListBox(this);
                this.Dispose();
            }


        }
        // Event på en knapp som möjliggör att man kan ändra kategori på en sparad feed.
        private void btn_Redigera_Click(object sender, EventArgs e)
        {


        }
    }
}
