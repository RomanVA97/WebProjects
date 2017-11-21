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
    [Activity(Label = "TwoActivity")]
    public class TwoActivity : Bck
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string mesto = Intent.GetStringExtra("mesto");

            SetContentView(Resource.Layout.Two);

            Button btn = FindViewById<Button>(Resource.Id.btnTwo);
            btn.Click += delegate
            {
                TrainContext trainContext = new TrainContext();
                List<Train> list = trainContext.GetList();
                TextView textView = FindViewById<TextView>(Resource.Id.tV2);
                TimePicker tP = FindViewById<TimePicker>(Resource.Id.tP);
                textView.Text = "";

                foreach (Train item in list)
                {
                    if (item.Punkt == mesto && item.Date.Hour >= tP.Hour) textView.Text += item.ToString();
                }


            };
        }
    }
}