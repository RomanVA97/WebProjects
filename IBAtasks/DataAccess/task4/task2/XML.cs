using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace task2
{
    class XML
    {
        public static List<Book> Read(string fileName)
        {
            List<Book> list = new List<Book>();
            XmlSerializer formatter = new XmlSerializer(typeof(List<Book>));
            using (TextReader xml = new StreamReader(fileName))
            {
                list = (List<Book>)formatter.Deserialize(xml);
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
