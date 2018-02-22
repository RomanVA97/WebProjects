using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Herbivore
    {

        public Action OnChange { get; set; }
        public void Raise()
        {
            OnChange?.Invoke();
        }
    }
}
