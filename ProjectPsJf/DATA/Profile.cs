using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Data
{
    /*
         Klassen Profile är en subklass från interfacet Isave. Denna klass, dvs "Profile", används för att spara en xml-fil till datorn som håller
         information om en sparad Profil.
   */
    public class Profile : Isave
    {
        //Syftet med denna metod är att skapa och spara ner en xml fil
        public void save(string name, string path, string kat, string frek)
        {
            XmlDocument doc = new XmlDocument();
            
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement element1 = doc.CreateElement(string.Empty, "body", string.Empty);
            doc.AppendChild(element1);

            XmlElement element2 = doc.CreateElement(string.Empty, "Name", string.Empty);
            XmlText text1 = doc.CreateTextNode(name);
            element2.AppendChild(text1);
            element1.AppendChild(element2);

            XmlElement element3 = doc.CreateElement(string.Empty, "Path", string.Empty);
            XmlText text2 = doc.CreateTextNode(path);
            element3.AppendChild(text2);
            element1.AppendChild(element3);

            XmlElement element4 = doc.CreateElement(string.Empty, "Kat", string.Empty);
            XmlText text3 = doc.CreateTextNode(kat);
            element4.AppendChild(text3);
            element1.AppendChild(element4);

            XmlElement element5 = doc.CreateElement(string.Empty, "Frek", string.Empty);
            XmlText text4 = doc.CreateTextNode(frek);
            element5.AppendChild(text4);
            element1.AppendChild(element5);

            XmlElement element6 = doc.CreateElement(string.Empty, "Played", string.Empty);

            XmlElement element7 = doc.CreateElement(string.Empty, "ID", string.Empty);
            element6.AppendChild(element7);


            element1.AppendChild(element6);


            doc.Save(@"savedFeeds/" + name + ".xml");
        }

    }
}
