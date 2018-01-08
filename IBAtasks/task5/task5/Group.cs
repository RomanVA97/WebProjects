using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class Group
    {
        List<IAnimal> items = new List<IAnimal>();

        public void Add(IAnimal item)
        {
            items.Add(item);
        }

        public List<IAnimal> GetList
        {
            get { return items; }
        }

        public bool Delete(IAnimal item)
        {
            return items.Remove(item);
        }

    }
}
