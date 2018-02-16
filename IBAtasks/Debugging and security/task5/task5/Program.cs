using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool chek = true;
            int k;
            List<double> list = new List<double>();
            while (chek)
            {
                Console.Clear();
                foreach (double item in list) Console.Write(item + " ");
                Console.WriteLine();
                Console.WriteLine("1 - Input");
                Console.WriteLine("2 - Max");
                Console.WriteLine("3 - Min");
                Console.WriteLine("4 - Avg");
                Console.WriteLine("0 - Выход");
                k = Convert.ToInt32(Console.ReadLine());
                switch (k)
                {
                    case 1:
                        {
                            Console.WriteLine("Вводите");
                            list.Add(Convert.ToInt32(Console.ReadLine()));
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine(list.Max());
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine(list.Min());
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine(list.Average());
                        }
                        break;
                    case 0:
                        {
                            chek = false;
                        }
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
