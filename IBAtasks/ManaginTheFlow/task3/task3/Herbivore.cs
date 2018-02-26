using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Herbivore
    {
        public static EventHandler Handler = EventH;

        public Action OnChange { get; set; }
        public void Raise()
        {
            Handler = EventH;
            Handler.Invoke(this, new EventArgs());
        }

        static void EventH(object sender, EventArgs e)
        {
            Console.WriteLine("Ок, уже");
            Handler = null;
            Plant.Handler?.Invoke(new object(), new EventArgs());
        }
    }
}
