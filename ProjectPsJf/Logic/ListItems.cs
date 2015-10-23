using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    //Denna klass används för att generera Items till listView
      public abstract class ListItems  
    {
        public string Title { get; set; }
        public string Stamp { get; set; }
        public string URL { get; set; }
        public string Namn { get; set; }
        public bool Played { get; set; }
    }
}
