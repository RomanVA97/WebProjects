using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker;
            Store store = new Store();
            for(int i = 0; i < 5; i++)
            {
                worker = new Worker { Id = i, Name = "Nm", FirstName = "FrstNm", LastName = "LstNm", Age = i+15, Position = "pos", Salary = i };
                try
                {
                    store.Add(worker);
                }
                catch(WorkerProcessingExeption e)
                {
                    Console.WriteLine(e.Message);
                }
            }



            Console.ReadKey();
        }
    }
}
