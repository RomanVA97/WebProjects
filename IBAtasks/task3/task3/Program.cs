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
            Random random = new Random();
            List<Employee> list = new List<Employee>();
            for (int i = 0; i < 5; i++)
            {
                Employee employee = new Employee();
                employee.Name = "name_" + (i + 1);
                employee.FirstName = "firstName_" + (i + 1);
                employee.LastName = "lastName_" + (i + 1);
                employee.Age = random.Next(18, 70);
                employee.Salary = random.Next(500, 1100);
                list.Add(employee);
            }


            foreach (Employee item in list)
                Console.WriteLine(item.ToString());
            Console.WriteLine("Salary");
            list = Employee.SalarySort(list);
            foreach (Employee item in list)
                Console.WriteLine(item.ToString());

            Console.WriteLine("Age");
            list = Employee.AgeSort(list);
            foreach (Employee item in list)
                Console.WriteLine(item.ToString());


            Console.ReadKey();
        }
    }
}
