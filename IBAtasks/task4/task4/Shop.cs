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

        public void CreateOrder(Client client, List<Product> productList)
        {
            List<int> productIdList = new List<int>();
            foreach (Product item in productList) productIdList.Add(item.Id);
            list.Add(new Order { Id = list.Count, ClientId = client.Id, ProductIdList = productIdList});
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
