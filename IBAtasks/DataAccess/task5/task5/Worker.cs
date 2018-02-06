using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    public enum WorkerPosition { Captain, Technican, Pilot };
    class Worker:IComparer<Worker>, IComparable<Worker>
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public WorkerPosition WorkerPosition { get; set; }


        public int Compare(Worker x, Worker y)
        {
            return x.WorkerPosition.CompareTo(y.WorkerPosition);
        }

        public int CompareTo(Worker other)
        {
            return Compare(this, other);
        }

        public override string ToString()
        {
            return Name +" "+ FirstName + " " + LastName + " " + WorkerPosition.ToString();
        }
    }
}
