using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    public enum WorkerPosition { Капитан,Летчик,Техник};
    class Worker:IComparer<Worker>, IComparable<Worker>
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public WorkerPosition WorkerPosition { get; set; }


        public int Compare(Worker x, Worker y)
        {
            int one = 0;
            int two = 0;
            if (x.WorkerPosition == WorkerPosition.Капитан) one = 2;
            else if(x.WorkerPosition == WorkerPosition.Техник) one = 1;
            if (y.WorkerPosition == WorkerPosition.Капитан) two = 2;
            else if (y.WorkerPosition == WorkerPosition.Техник) two = 1;
            return one.CompareTo(two);
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
