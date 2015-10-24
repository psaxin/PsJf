using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{

     // Implementerar klassen ListItems, har förutom ListItems properties frekvens (När en pod ska uppdateras samt kategori).
     public class SavedItems : ListItems
    {
        public string Frekvens { get; set; }
        public string Kategori { get; set; }
        
    }
}
