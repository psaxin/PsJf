using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPsJf
{
    //Denna klass används för att generera Items till listView
    public class listViewItems
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string URL { get; set; }
        public string Namn { get; set; }
        public string Kategori { get; set; }
        public string Frekvens { get; set; }
        public bool Seen { get; set; }
    }
}
