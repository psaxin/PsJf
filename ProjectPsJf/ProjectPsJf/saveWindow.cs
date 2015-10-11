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

namespace ProjectPsJf
{
    public partial class saveWindow : Form
    {
        private MainWindow mainForm;
      
        public saveWindow (String url, MainWindow main): this()
        {
            mainForm =  main;
            lblUrl.Text += " " + url;

        }

        public saveWindow()
        {

            InitializeComponent();
        }

        private void btnSpara_Click(object sender, EventArgs e)
        {
            
            mainForm.addToListBox(this);
            this.Dispose();

        }

       

    }
}
