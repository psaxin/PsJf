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
        void save(string name, string path, string kat, string frek);
    }
}