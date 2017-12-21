using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Mark
    {
        public string Name{ get; set; }
        public int MarkValue { get; set; }

        public override string ToString()
        {
            return "Name : " + Name + " - " + MarkValue;
        }
    }
}
