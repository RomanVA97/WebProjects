using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Carnivore
    {
        public static EventHandler Handler = EventH;
        
        static void EventH(object sender, EventArgs e)
        {
            Console.WriteLine("Съешь меня");
            Handler = null;
            Herbivore.Handler?.Invoke(new object(), new EventArgs());
        }
    }
}
