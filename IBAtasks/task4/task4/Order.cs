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
        public int ProductId { get; set; }
        public bool IsPaid { get; set; }


        public override string ToString()
        {
            return "Client id - " + ClientId + " Product id - " + ProductId + " IsPaid - " + IsPaid;
        }

    }
}
