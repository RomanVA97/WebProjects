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
using AppDrwer.Resources.model;

namespace AppDrwer
{
    [Activity(Label = "ThreeActivity")]
    public class ThreeActivity : Bck
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string mesto = Intent.GetStringExtra("mesto");

            SetContentView(Resource.Layout.Three);


            TrainContext trainContext = new TrainContext();
            List<Train> list = trainContext.GetList();

            TextView textView = FindViewById<TextView>(Resource.Id.tV3);
            textView.Text = "";
            foreach (Train item in list)
            {
                if (item.Punkt == mesto && item.Ob > 0) textView.Text += item.ToString();
            }
        }
    }
}