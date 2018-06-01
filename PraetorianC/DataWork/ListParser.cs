using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraetorianC.DataWork
{
    class ListParser
    {
        public ListParser() { }
        public List<String> Parse(byte[] rawData, char divider)
        {
            String data = Encoding.Default.GetString(rawData);
            return data.Split(divider).ToList();
        }
    }
}
