using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Plant plant = new Plant();
            Carnivore carnivore = new Carnivore();
            Herbivore herbivore = new Herbivore();
            plant.OnChange += () => Console.WriteLine("Расту ради того чтобы скушать травоядное");
            carnivore.OnChange += () => Console.WriteLine("Съешь меня");
            herbivore.OnChange += () => Console.WriteLine("Ок, уже");
            plant.Raise();
            carnivore.Raise();
            herbivore.Raise();

            Console.ReadKey();
        }
    }
}
