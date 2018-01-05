using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Cat : IAnimal
    {
        public void Breathe()
        {
            Console.WriteLine("дышу легкими");
        }

        public void Move()
        {
            Console.WriteLine("когтями по паркету");
        }

        public void Voice()
        {
            Console.WriteLine("мяу");
        }
    }
}
