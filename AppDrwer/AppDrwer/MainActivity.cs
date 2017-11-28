using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using Android.Content;

namespace AppDrwer
{
    [Activity(Label = "AppDrawer", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        DrawerLayout drawerLayout;
        List<string> menuItems = new List<string>();
        ArrayAdapter arrayAdapter;
        ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //List<Train> train = Train.GetObject().GetList();
            /*string[] menuItems = new string[]{ "a)	список поездов, следующих до заданного пункта назначения",
                "b)	список поездов, следующих до заданного пункта назначения и отправляющихся после заданного часа",
                "c)	список поездов, отправляющихся до заданного пункта назначения и имеющих общие места",
                "d) список всех поездов"};*/
            //var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, menuItems);

            menuItems.Add("Cписок поездов, следующих до заданного пункта назначения");
            menuItems.Add("Cписок поездов, следующих до заданного пункта назначения и отправляющихся после заданного часа");
            menuItems.Add("Cписок поездов, отправляющихся до заданного пункта назначения и имеющих общие места");
            menuItems.Add("Cписок всех поездов");
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            listView = FindViewById<ListView>(Resource.Id.listView);
            arrayAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, menuItems);
            listView.Adapter = arrayAdapter;
            listView.ItemClick += (sender, e) =>
            {
                AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
                Intent intent = new Intent(this, typeof(OneActivity));
                //Toast.MakeText(this, e.Position.ToString(), ToastLength.Long).Show();
                switch (e.Position)
                {
                    case 1:
                        {
                            intent = new Intent(this, typeof(TwoActivity));
                        }
                        break;
                    case 2:
                        {
                            intent = new Intent(this, typeof(ThreeActivity));
                        }
                        break;
                    case 3:
                        {
                            intent = new Intent(this, typeof(ShowActivity));
                        }
                        break;
                }
                intent.PutExtra("mesto", textView.Text);
                StartActivity(intent);

            };


        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);


            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            Intent intent;
            switch (item.ItemId)
            {
               case Resource.Id.action_time:
                    {
                        intent = new Intent(this, typeof(TimeActivity));
                        StartActivity(intent);
                    }
                    break;
                
                case Resource.Id.action_show:
                    {
                        intent = new Intent(this, typeof(ShowActivity));
                        StartActivity(intent);
                    }
                    break;
                case Resource.Id.action_r:
                    {
                        intent = new Intent(this, typeof(ShowRActivity));
                        StartActivity(intent);
                    }
                    break;


                case Resource.Id.action_n:
                    {
                        Toast.MakeText(this, "Это лаба нумар ШыСцЬ", ToastLength.Long).Show();
                    }
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }


    }
}

