using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Girl:Person
    {
        int numberOfShoes = 2;



        public void BuyShoes()
        {
            numberOfShoes++;
        }


        public override void Move()
        {
            Console.WriteLine("я хожу в туфлях");
        }



        public override string ToString()
        {
            return Name + ": у меня есть " + numberOfShoes + " пар обуви";
        }


    }
}
