﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<Student> list = new List<Student>();
            for(int i = 0; i < 5; i++)
            {
                Student student = new Student();
                student.Name = "name_" + (i + 1);
                student.FirstName = "firstName_" + (i + 1);
                student.LastName = "lastName_" + (i + 1);
                student.Mark = new Mark[4];
                for (int j = 0; j < 4; j++)
                {
                    student.Mark[j] = new Mark { Name = "object_" + (j + 1), MarkValue = random.Next(5, 11) };
                }
                list.Add(student);
            }

            foreach(Student item in list)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("Avg : " + item.GetAvgMark());
                foreach (Mark itemMark in item.Mark)
                    Console.WriteLine(itemMark.ToString());
            }
            foreach (Student item in list)
            {
                Console.WriteLine(item.ToString());
                Student.ResetAllMarks(item.Mark);
                foreach (Mark itemMark in item.Mark)
                    Console.WriteLine(itemMark.ToString());
            }
            foreach (Student item in list)
            {
                Console.WriteLine(item.ToString());
                Student.SetNullReference(ref item.mark);
                //Student.ResetAllMarks(item.Marks);
                if (item.Mark == null)
                {
                    Console.WriteLine("Reference is NULL");
                    continue;
                }
                foreach (Mark itemMark in item.Mark)
                    Console.WriteLine(itemMark.ToString());
            }


            Console.ReadKey();
        }
    }
}
