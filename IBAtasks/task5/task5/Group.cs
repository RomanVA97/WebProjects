using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Group<T>
    {
        List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> GetList
        {
            get { return items; }
        }

        public bool Delete(T item)
        {
            return items.Remove(item);
        }

    }
}
