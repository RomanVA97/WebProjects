using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Employee
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }


        public static List<Employee> SalarySort(List<Employee> list)
        {
            return list.OrderByDescending(x => x.Salary).ToList();
        }

        public static List<Employee> AgeSort(List<Employee> list)
        {
            return list.OrderBy(x => x.Age).ToList();
        }

        public override string ToString()
        {
            return "FIO : " + Name + " " + FirstName + " " + LastName + " Age : " + Age + " Salary - " + Salary;
        }

    }
}
