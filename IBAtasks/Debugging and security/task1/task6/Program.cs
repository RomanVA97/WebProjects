using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Program
    {
        static void Main(string[] args)
        {
            bool chek = true;
            int k;
            string str = "";
            bool result = true;
            while (chek)
            {
                Console.WriteLine("Что проверяем?");
                Console.WriteLine("1-Email");
                Console.WriteLine("2-URL");
                Console.WriteLine("3-Paths");
                Console.WriteLine("4-Price");
                Console.WriteLine("0-Exit");
                k = Convert.ToInt32(Console.ReadLine());
                if (k == 0) break;
                Console.Write("Input string: ");
                str = Console.ReadLine();
                switch(k)
                {
                    case 1:
                        {
                            result = Validate.Email(str);
                        }
                        break;
                    case 2:
                        {
                            result = Validate.URL(str);
                        }
                        break;
                    case 3:
                        {
                            result = Validate.Paths(str);
                        }
                        break;
                    case 4:
                        {
                            result = Validate.Price(str);
                        }
                        break;
                    case 0:
                        {
                            chek = false;
                        }
                        break;
                }
                
                if (result)
                {
                    Console.WriteLine("Данные верны");
                }
                else
                {
                    Console.WriteLine("Данные неверны");
                }
            }














            
        }
    }
}
