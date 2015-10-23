using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace Logic
{
   public static class validate
    
   {


        //returnar true om en string är null
        public static bool isEmpty(this string x)
        {

            return (x == null ? true : (x.Trim() == ""));

        }

        public static bool isDigit(string input)
        {
            Regex regex = new Regex(@"(?:\d*)?\d+");
            if(regex.IsMatch(input))
            {
                return true;
            }
            else
            {
                return false;

            }

        }

        public static bool isLetters(string input)
        {
            Regex regex = new Regex("[A-Za-z]");
            if (regex.IsMatch(input))
            {
                return true;
            }
            else
            {
                return false;

            }

        }

        public static bool notNullOrEmpty(string input)
        {
            
            if (!(input.Length == 0 || input == null))
            {
                return true;
            }
            else
            {
                return false;

            }

        }

        public static bool IsValidFeedUrl(string url)
        {

            bool isValid = true;
            try
            {
                XmlReader reader = XmlReader.Create(url);
                Rss20FeedFormatter formatter = new Rss20FeedFormatter();
                formatter.ReadFrom(reader);
                reader.Close();
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }       

    }
}
