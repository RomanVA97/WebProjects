using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Fish : IAnimal
    {
        public string Name { get; set; }

        public void Breathe()
        {
            Console.WriteLine("жабры");
        }

        public void Move()
        {
            Console.WriteLine("плыву");
        }

        public void Voice()
        {
            Console.WriteLine("я молчу я рыба");
        }
    }
}
