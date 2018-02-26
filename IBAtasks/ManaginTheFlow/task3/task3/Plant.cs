using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Plant
    {
        public static object obj = new object();
        public static EventHandler Handler = EventH;

        public void Raise()
        {
            
            Handler += EventH;
            Handler.Invoke(this, new EventArgs());
        }


        static void EventH(object sender, EventArgs e)
        {
            if (obj!=null)
            {
                Console.WriteLine("Расту ради того чтобы скушать травоядное");
                Handler = null;
                Carnivore.Handler?.Invoke(new object(), new EventArgs());
                
            }
            
        }

    }
}
