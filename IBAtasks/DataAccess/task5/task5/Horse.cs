using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Horse : IAnimal
    {
        public string Name { get; set; }
        public void Breathe()
        {
            Console.WriteLine("дышу легкими");
        }

        public void Move()
        {
            Console.WriteLine("тыгыдык тыгыдык");
        }

        public void Voice()
        {
            Console.WriteLine("весь такой как конь");
        }


        public override string ToString()
        {
            return Name;
        }

    }
}
