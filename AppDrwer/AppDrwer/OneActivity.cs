﻿using System;
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
    [Activity(Label = "OneActivity")]
    public class OneActivity : Bck
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string mesto = Intent.GetStringExtra("mesto");

            SetContentView(Resource.Layout.One);


            TrainContext trainContext = new TrainContext();
            List<Train> list = trainContext.GetList();

            TextView textView = FindViewById<TextView>(Resource.Id.tV1);
            textView.Text = "";
            foreach (Train item in list)
            {
                if (item.Punkt == mesto) textView.Text += item.ToString();
            }
        }
    }
}