using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectPsJf
{
   
    //denna klass finns mest för att experimentera i för tillfället
    public class Class1
    {
        private MainWindow mainForm;

        public Class1(MainWindow main) {
            mainForm = main;


        }
        public void test() {

            mainForm.printStatusMessage("hej");

            //lbStatusMessage.Dispatcher.BeginInvoke(new Action(delegate ()
            //{
            //    lbStatusMessage.Items.Add("ddd");
            //}));
        }

 
    }


    
    }
