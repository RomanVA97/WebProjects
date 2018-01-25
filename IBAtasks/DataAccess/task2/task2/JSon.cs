using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;



namespace task2
{
    

    public class JSon
    {

        
        public static List<Book> Read(StreamReader str)
        {
            JsonTextReader jtr = new JsonTextReader(str);
            JsonSerializer js = new JsonSerializer();
            List<Book> list=(List<Book>)js.Deserialize(jtr, typeof(List<Book>));

            return list;
        }

        public static void Write(List<Book> an, StreamWriter sw)
        {
            JsonTextWriter jw = new JsonTextWriter(sw);

            JsonSerializer js = new JsonSerializer();
            js.Formatting = Formatting.Indented;
            js.Serialize(jw, an);
            jw.Close();
        }
    }
}
