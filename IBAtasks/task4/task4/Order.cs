using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<int> ProductIdList { get; set; }
        public bool IsPaid { get; set; }


        public override string ToString()
        {
            string s = "Client id - " + ClientId + " IsPaid - " + IsPaid + " Product id: ";
            foreach (int item in ProductIdList) s += item.ToString() + " ";
            return s;
        }

    }
}
