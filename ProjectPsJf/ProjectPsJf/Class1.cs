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
     class Class1
    {
        public Array getArray() {

            string[] hehe;
            
            hehe = Directory.GetFiles(@"savedFeeds\src\");

            return hehe;
        }

        internal void dispose()
        {
            throw new NotImplementedException();
        }
    }


    
    }
