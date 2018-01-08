using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Program
    {
        static void Main(string[] args)
        {

            Group fishGroup = new Group();
            Group horseGroup = new Group();
            Group eagleGroup = new Group();

            for(int i = 0; i < 5; i++)
            {
                fishGroup.Add(new Fish { Name = "fish - " + (i + 1) });
            }

            for (int i = 0; i < 5; i++)
            {
                horseGroup.Add(new Horse { Name = "horse - " + (i + 1) });
            }

            for (int i = 0; i < 5; i++)
            {
                eagleGroup.Add(new Eagle { Name = "eagle - " + (i + 1) });
            }

            foreach (Fish item in fishGroup.GetList) Console.WriteLine(item.Name);

            foreach (Horse item in horseGroup.GetList) Console.WriteLine(item.Name);

            foreach (Eagle item in eagleGroup.GetList) Console.WriteLine(item.Name);


            Console.ReadKey();
        }
    }
}
