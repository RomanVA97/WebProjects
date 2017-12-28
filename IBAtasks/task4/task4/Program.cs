using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            List<Product> productList = new List<Product>();
            List<Client> clientList = new List<Client>();
            for (int i = 0; i < 5; i++)
            {
                productList.Add(new Product { Id = i, Name = "Product name " + (i + 1), Cost = 1000 * (i + 1) });
            }

            for (int i = 0; i < 5; i++)
            {
                clientList.Add(new Client { Id = i, Name = "name_" + (i + 1),FirstName = "firstName_" + (i + 1),LastName = "lastName_" + (i + 1), Adress = "adress " + i });
            }

            for (int i = 0; i < 5; i++)
            {
                shop.CreateOrder(clientList.ElementAt(i), productList);
            }
            shop.PaymentOrder(2);
            shop.PaymentOrder(0);

            foreach (Order item in shop.GetOrdersList) Console.WriteLine(item.ToString());

            Console.ReadKey();
        }
    }
}
