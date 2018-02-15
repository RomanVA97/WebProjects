using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 2;
            int[] y = { 10, 15, 123, 65 };
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Algorithm1(x, y);
            Console.WriteLine("Затраченное время - " + stopwatch.ElapsedTicks);
            stopwatch.Start();
            Algorithm2(x, y);
            Console.WriteLine("Затраченное время - " + stopwatch.ElapsedTicks);


            Console.ReadKey();

        }

        static void Algorithm1(int x, int[] y)
        {
            
            for(int i=0; i<y.Length;i++)
               Console.WriteLine(Evklid(x, y[i]));
            
        }

        static void Algorithm2(int x, int[] y)
        {
            for (int i = 0; i < y.Length; i++)
                Console.WriteLine(BinEvklid(x, y[i]));
        }

        static long Evklid(long a, long b)
        {

            if (a == b)
            {
                return a;
            }
            if (a > b)
            {
                long tmp = a;
                a = b;
                b = tmp;
            }
            return Evklid(a, b - a);
        }

        static long BinEvklid(long a, long b)
        {
            if (a == 0L)
                return b;
            if (b == 0L)
                return a;
            if (a == b)
                return a;
            if (a == 1L || b == 1L)
                return 1L;
            if (a % 2L == 0L && b % 2L == 0L)
                return 2L * BinEvklid(a / 2L, b / 2L);
            if (a % 2L == 0L && b % 2L != 0L)
                return BinEvklid(a / 2L, b);
            if (a % 2L != 0L && b % 2L == 0L)
                return BinEvklid(a, b / 2L);
            if (a < b)
                return BinEvklid((b - a) / 2L, a);
            else
                return BinEvklid((a - b) / 2L, b);
        }




    }
}
