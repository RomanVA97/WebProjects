using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static int seconds = 0;
        static bool start = false;
        static bool work = true;
        static void Main(string[] args)
        {
            bool chek = true;
            int k;
            Thread myThread = new Thread(new ThreadStart(Enc));
            myThread.Start();

            while (chek)
            {
                Console.Clear();
                Console.WriteLine("1 - Запустить секундомер");
                Console.WriteLine("2 - Остановить секундомер");
                Console.WriteLine("3 - Взять значение");
                Console.WriteLine("0 - Выход");
                k = Convert.ToInt32(Console.ReadLine());
                switch (k)
                {
                    case 1:
                        {
                            start = true;
                            seconds = 0;
                        }
                        break;
                    case 2:
                        {
                            start = false;
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Прошло {0} секунд", seconds);
                            Console.ReadKey();
                        }
                        break;
                    case 0:
                        {
                            work = false;
                            chek = false;
                        }
                        break;
                }
            }
        }

        public static void Enc()
        {
            while (work)
            {
                if (start)
                {
                    seconds++;
                    Thread.Sleep(1000);
                }
            }
        }

    }
}
