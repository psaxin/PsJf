using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GUI
{
    public class Feed : Isave
    {
        private XDocument xDoc = new XDocument();
        public void save(string name, string path, string kat, string frek)
        {
            xDoc = XDocument.Load(path);
            xDoc.Save(@"savedFeeds/src/" + name + ".xml");
        }

    }
}
