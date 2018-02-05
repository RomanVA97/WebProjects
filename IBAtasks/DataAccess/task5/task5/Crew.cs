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
        Crew[] crew = new Crew[100];
        int count = 0;


        public List<Worker> WorkerList { get; set; }


        public Crew this[int index] { get => crew[index]; set => crew[index] = value; }

        public int Count { get { return count; } }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Crew item)
        {
            crew[count] = item;
            count++;
        }

        public void Clear()
        {
            count = 0;
        }

        public bool Contains(Crew item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Crew[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Crew> GetEnumerator()
        {
            for (int i = 0; i < count; i++) yield return crew[i];
        }

        public int IndexOf(Crew item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Crew item)
        {
            crew[index] = item;
        }

        public bool Remove(Crew item)
        {
            for (int i = 0; i < count; i++)
            {
                if (crew[i] == item)
                {
                    crew[i] = null;
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            crew[index] = null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
