using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Boy:Person
    {
        int numberOfWins = 0;





        public void Play()
        {
            numberOfWins++;
        }


        public override void Move()
        {
            Console.WriteLine("Я хожу в кроссах");
        }

        public override string ToString()
        {
            return Name + ": у меня есть " + numberOfWins + " побед";
        }



    }
}
