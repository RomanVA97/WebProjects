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
            List<double> list2 = new List<double>();
            while (chek)
            {
                Console.Clear();
                Console.Write("Событие:");
                foreach (double item in list) Console.Write(item + " ");
                Console.WriteLine();
                Console.Write("Вероятность:");
                foreach (double item in list2) Console.Write(item + " ");
                Console.WriteLine();
                if (list2.Sum() > 1) Console.WriteLine("Сумма вероятностей не может быть больше 1");
                Console.WriteLine("В массиве располагаются вероятности наступления событий и события");
                Console.WriteLine();
                Console.WriteLine("1 - Input");
                Console.WriteLine("2 - Выполнить действия");
                Console.WriteLine("0 - Выход");
                k = Convert.ToInt32(Console.ReadLine());
                switch (k)
                {
                    case 1:
                        {
                            Console.WriteLine("Событие ");
                            list.Add(Convert.ToDouble(Console.ReadLine()));
                            Console.WriteLine("Вероятность ");
                            list2.Add(Convert.ToDouble(Console.ReadLine()));
                        }
                        break;
                    case 2:
                        {
                            double result = 0;
                            double M=0;
                            double D = 0;
                            for (int i = 0; i < list.Count; i++)
                                M += list[i] * list2[i];
                            Console.WriteLine("Матожидание - " + M);
                            for (int i = 0; i < list.Count; i++)
                                D += Math.Pow(list[i]-M, 2) * list2[i];
                            Console.WriteLine("Дисперсия - " + D);
                            Console.WriteLine("Стандартное отклонение - " + Math.Sqrt(D));


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
