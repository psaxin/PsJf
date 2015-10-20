using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
   public static class validate
    {

        //returnar true om en string är null
        public static bool isEmpty(this string x)
        {

            return (x == null ? true : (x.Trim() == ""));

        }

    }
}
