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

namespace AppDrwer.Resources.models
{
    class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }

        public override string ToString()
        {
            return Id + " " + Name + " " + Lat + " " + Long;
        }

        public double GetR(double lttg, double lng)
        {
            return Math.Sqrt((Lat - lttg) * (Lat - lttg) + (Long - lng) * (Long - lng));
        }

    }
}