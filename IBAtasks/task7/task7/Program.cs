using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array = new string[4];
            array[0] = "doG";
            array[2] = "Cat";
            array[3] = "bIRd";
            TextTransformer textTransformer = new TextTransformer(array);

            foreach (string item in textTransformer.GetEnumerator())
                Console.WriteLine(item);

            foreach (string item in textTransformer.GetTransformStrings())
                Console.WriteLine(item);



            Console.ReadLine();
        }
    }
}
