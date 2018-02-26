using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Plant.Handler.Invoke(new object(), new EventArgs());



            Console.ReadKey();
        }
    }
}
