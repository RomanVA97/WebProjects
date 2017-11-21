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
    [Activity(Label = "ShowActivity")]
    class ShowActivity:Bck
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Show);

            ListView listView = FindViewById<ListView>(Resource.Id.listViewShow);

            TrainContext trainContext = new TrainContext();
            List<Train> list = trainContext.GetList();
            var adapter = new ArrayAdapter<Train>(this, Android.Resource.Layout.SimpleListItem1, list);

            listView.Adapter = adapter;
            listView.ItemClick += (sender, e) =>
            {

                TrainContext tContext = new TrainContext();
                List<Train> tr = tContext.GetList();
                Train item = tr.ElementAt(e.Position);
                //Toast.MakeText(this, item.ToString(), ToastLength.Long).Show();


                SetContentView(Resource.Layout.ItemShow);
                TextView text = FindViewById<TextView>(Resource.Id.textView2);
                text.Text = item.Punkt;
                text = FindViewById<TextView>(Resource.Id.textView4);
                text.Text = item.Number.ToString();
                text = FindViewById<TextView>(Resource.Id.textView6);
                text.Text = item.Date.Hour + " : " + item.Date.Minute;
                text = FindViewById<TextView>(Resource.Id.textView8);
                text.Text = item.Ob + " " + item.Kupe + " " + item.Pl + " " + item.Lukc;
            };

        }
        
    }
}