using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Shop
    {
        List<Order> list = new List<Order>();

        public void CreateOrder(Client client, Product product)
        {
            list.Add(new Order { Id = list.Count, ClientId = client.Id, ProductId = product.Id});
        }

        public void PaymentOrder(int orderId)
        {
            list.Find(x => x.Id == orderId).IsPaid = true;
        }


        public List<Order> GetOrdersList
        {
            get { return list; }
        }

    }
}
