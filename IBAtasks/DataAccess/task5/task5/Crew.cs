using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Crew : IList<Crew>
    {
        List<Crew> list = new List<Crew>();
        int count = 0;
        
        public List<Worker> WorkerList { get; set; }
        

        public Crew this[int index] { get => list[index]; set => list[index] = value; }

        public int Count { get { return list.Count; } }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Crew item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Crew item)
        {
            return list.Contains(item);
        }

        public void CopyTo(Crew[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Crew> GetEnumerator()
        {
            return list.GetEnumerator();
            
        }

        public int IndexOf(Crew item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, Crew item)
        {
            list.Insert(index, item);
        }

        public bool Remove(Crew item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
