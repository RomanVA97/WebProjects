﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Plant
    {
        public static EventHandler Handler = EventH;
        
        static void EventH(object sender, EventArgs e)
        {
            
            Console.WriteLine("Расту ради того чтобы скушать травоядное");
            Handler = null;
            Carnivore.Handler?.Invoke(new object(), new EventArgs());
                
            
            
        }

    }
}
