using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace task2
{
    class XML
    {
        public static List<Book> Read(string fileName)
        {
            List<Book> list = new List<Book>();
            string[] array = new string[4];
            int kol;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                kol = 0;

                foreach (XmlNode item in node.ChildNodes)
                {
                    array[kol] = item.InnerText;
                    kol++;
                }

                list.Add(new Book { Name = array[0], Autor = array[1], Description= array[2]});
            }
            return list;
        }

        public static void Write(List<Book> list, StreamWriter sw)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Book>));
            using (TextWriter writerxml = sw)
            {
                formatter.Serialize(writerxml, list);
            }
        }


    }
}
