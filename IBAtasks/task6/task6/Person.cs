using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Person
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }


        public virtual void Move()
        {
            Console.WriteLine("я хожу");
        }

        public void Speak()
        {
            Console.WriteLine("Я говорю");
        }





    }
}
