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
            Boy boy = new Boy { Name = "Ivan", FirstName = "Ivanov", LastName = "Ivanovich", Age = 22 };
            Girl girl = new Girl { Name = "Masha", FirstName = "Kren", LastName = "Petrovovna", Age = 17 };
            boy.Move();
            girl.Move();
            boy.Play();
            girl.BuyShoes();

            Console.WriteLine(boy.ToString());
            Console.WriteLine(girl.ToString());



            Console.ReadKey();
        }
    }
}
