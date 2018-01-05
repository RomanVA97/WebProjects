using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Eagle : IAnimal
    {
        public string Name { get; set; }

        public void Breathe()
        {
            Console.WriteLine("Дышу");
        }

        public void Move()
        {
            Console.WriteLine("Лечу");
        }

        public void Voice()
        {
            Console.WriteLine("курлык");
        }
    }
}
