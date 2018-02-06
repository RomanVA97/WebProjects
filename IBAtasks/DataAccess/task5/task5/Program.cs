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
            List<Worker> list = new List<Worker>();
            for(int i = 0; i < 3; i++)
            {
                list.Add(new Worker { Name = "name "+(i+1), FirstName = "FName "+(i+1), LastName= "LName "+(i+1), WorkerPosition = WorkerPosition.Pilot });
            }
            list.Add(new Worker { Name = "name " + 4, FirstName = "FName " + 4, LastName = "LName " + 4, WorkerPosition = WorkerPosition.Captain });
            list.Add(new Worker { Name = "name " + 5, FirstName = "FName " + 5, LastName = "LName " + 5, WorkerPosition = WorkerPosition.Technican });

            Crew crew = new Crew();
            crew.WorkerList = list;
            foreach (Worker item in crew.WorkerList) Console.WriteLine(item.ToString());
            crew.WorkerList.Sort();
            Console.WriteLine();
            foreach (Worker item in crew.WorkerList) Console.WriteLine(item.ToString());









            Console.ReadKey();
        }
    }
}
