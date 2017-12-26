using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Student
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Mark[] Marks { get; set; }

        public override string ToString()
        {
            return "FIO : " + Name + " " + FirstName + " " + LastName;
        }

        public double GetAvgMark()
        {
            return Marks.Average(x => x.MarkValue);
        }

        public static void ResetAllMarks(params Mark[] items)
        {
            foreach (Mark item in items) item.MarkValue = 0;
        }

    }
}
