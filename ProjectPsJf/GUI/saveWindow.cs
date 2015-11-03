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
        public string category;
        private MainWindow mainForm;
        private static string button;
        //  En overloaded konstruktor som först tar två parametrar sedan anropar default konstruktorn som bygger formen. Tar ett objekt av klassen MainWindow för att kunna använda dess metoder.
        public saveWindow(string url, string whichButton, MainWindow main): this()
        {
            button = whichButton;
            mainForm = main;
            tbUrl.Text = url;
            mainForm.fillCategory(cbCategories);

        }
        //  Standard konstruktor som bygger klassen.
        public saveWindow()
        {

            InitializeComponent();

        }
        // Event på en knapp som möjliggör att man kan spara ner eller editera  en xml fil. Anropar en spara funktion i mainWindow.
        private void btnSpara_Click(object sender, EventArgs e)
        {
            if (button == "edit")
            {
                if (cbCategories.SelectedItem != null)
                    category = cbCategories.SelectedItem.ToString();
                mainForm.redigera(this);
                this.Dispose();
            }

            else
            {
                if(cbCategories.SelectedItem != null)
                category = cbCategories.SelectedItem.ToString();

                mainForm.addToListBox(this);
                this.Dispose();
            }
        }

    }
}
