using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;
namespace GUI
{
    public partial class addCategoryForm : Form
    {
        MainWindow mainForm;
        string oldName;

        public addCategoryForm()
        {
            InitializeComponent();
        }
        public addCategoryForm(string name, MainWindow mainWindow) : this()
            {
                    oldName = name;
                 mainForm = mainWindow;
        }
        public addCategoryForm(MainWindow mainWindow) : this()
        {
            mainForm = mainWindow;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    
        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (oldName != null)
            {
                editCategory(oldName);
            }
            else
            {
                if (validate.isLetters(tbAddCategory.Text))
                {
                 HanteraRss.addCategory(tbAddCategory.Text);
                mainForm.fillCategory();
                this.Dispose();
                }
                else{
                    mainForm.printStatusMessage("Kategori får bara bestå av bokstäver!");
                    }
            }
        }
        private void editCategory(string s) {
        
            HanteraRss.editCategory(s, tbAddCategory.Text);
            mainForm.fillCategory();
            this.Dispose();
        }


    }
}
