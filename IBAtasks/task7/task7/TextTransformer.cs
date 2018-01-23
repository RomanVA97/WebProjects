using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task7
{
    class TextTransformer
    {
        string[] array;


        public TextTransformer(string[] array)
        {
            this.array = array;
        }


        public IEnumerable<string> GetTransformStrings()
        {
            string str;
            foreach(string item in array)
            {
                if (item == null) str = "something valuable";
                else
                {
                    str = item.ToUpper();
                }

                yield return str;
            }
        }

        public IEnumerable<string> GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
                yield return array[i];
        }
        
    }
}
