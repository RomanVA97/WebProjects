using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppDrwer.Resources.model
{
    class Train
    {
        public int Id { get; set; }
        public int StatitonId { get; set; }
        public string Punkt { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public int Ob { get; set; }
        public int Kupe { get; set; }
        public int Pl { get; set; }
        public int Lukc { get; set; }


        public override string ToString()
        {
            return Punkt + " " + Number + " " + StatitonId + " " + Date.Hour + ":" + Date.Minute + " " + Ob + " " + Kupe + " " + Pl + " " + Lukc;
        }

    }
}