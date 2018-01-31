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

            Group group = new Group();
            Fish fish = new Fish { Name = "fish" };
            group.Add(fish);
            Horse horse = new Horse { Name = "horse" };
            group.Add(horse);
            group.Add(new Eagle { Name = "eagle" });
            group.Add(new Cat { Name = "cat" });
            group.Add(new Cat { Name = "cat2" });
            group.Add(new Tiger { Name = "tiger" });

            Group group2 = new Group();
            group2.Add(fish);
            group2.Add(horse);




            List<IAnimal> result;


            foreach (IAnimal item in group.ToList<IAnimal>().OrderByDescending(c => c.ToString())) Console.WriteLine(item.ToString());
            Console.WriteLine();
            foreach (IAnimal item in group.ToList<IAnimal>().Where(c=>c.ToString().StartsWith("c"))) Console.WriteLine(item.ToString());
            result = group.Except(group2).ToList();

            Console.WriteLine();
            Console.WriteLine("Exept");
            
            foreach (IAnimal item in result) Console.WriteLine(item.ToString());
            result = group.Intersect(group2).ToList();
            Console.WriteLine();
            Console.WriteLine("Intersect");
            
            foreach (IAnimal item in result) Console.WriteLine(item.ToString());
            result = group.Union(group2).ToList();
            Console.WriteLine();
            Console.WriteLine("Union");
            foreach (IAnimal item in result.OrderBy(c=> c.ToString())) Console.WriteLine(item.ToString());


            Console.ReadKey();
        }
    }
}
