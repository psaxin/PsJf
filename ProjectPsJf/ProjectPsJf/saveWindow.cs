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
      

        public saveWindow (String url, XDocument xDoc): this()
        {
            
            lblUrl.Text += " " + url;

        }

        public saveWindow()
        {

            InitializeComponent();
        }

        private void btnSpara_Click(object sender, EventArgs e)
        {

           
            this.Dispose();

        }

       

    }
}
