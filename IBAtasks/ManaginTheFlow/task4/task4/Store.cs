using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Store
    {
        List<Worker> list = new List<Worker>();


        public void Add(Worker worker)
        {
            if (worker.Salary > 0 && worker.Age > 16) list.Add(worker);
            else throw new WorkerProcessingExeption(worker.Id, "IncorrectData");
        }


    }
}
