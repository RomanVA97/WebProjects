using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Tiger : IAnimal
    {
        public string Name { get; set; }
        public void Breathe()
        {
            Console.WriteLine("дышу легкими");
        }

        public void Move()
        {
            Console.WriteLine("по джунглям хожу");
        }

        public void Voice()
        {
            Console.WriteLine("как тигр такой рев");
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
