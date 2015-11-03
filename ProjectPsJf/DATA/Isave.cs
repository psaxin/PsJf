using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /*
    Detta interface används för att visa att alla klasser av Isave har dess metoder, som save();
    */
    public interface Isave
    {
        //Syftet med denna metod är att skapa och spara ner en xml fil
        void save(string name, string path, string kat, string frek);
    }
}