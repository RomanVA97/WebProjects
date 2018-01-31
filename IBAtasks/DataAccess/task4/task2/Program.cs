using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> list = new List<Book>();
            list.Add(new Book { Name = "имя 1", Autor = "автор 1", Description = "описание 1" });
            list.Add(new Book { Name = "имя 2", Autor = "автор 2", Description = "описание 2" });
            list.Add(new Book { Name = "имя 3", Autor = "автор 3", Description = "описание 3" });
            Console.WriteLine("Введите полное имя файла для записи");
            string name = Console.ReadLine();
            switch (name)
            {
                case "json":
                    {
                        JSon.Write(list, new StreamWriter(name));
                    }
                    break;
                case "xml":
                    {
                        XML.Write(list, new StreamWriter(name));
                    }
                    break;   
            }
            Console.WriteLine("Введите полное имя файла для чтения");
            name = Console.ReadLine();
            switch (name)
            {
                case "json":
                    {
                        list = JSon.Read(new StreamReader(name));
                    }
                    break;
                case "xml":
                    {
                        list = XML.Read(name);
                    }
                    break;
            }
            foreach (Book item in list)
                Console.WriteLine(item.Name + " - " + item.Autor);
            Console.ReadKey();
        }
    }
}
