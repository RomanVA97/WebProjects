using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using AppDrwer.Resources.model;
using System.Threading.Tasks;
using Android.Util;
using AppDrwer.Resources.models;

namespace AppDrwer
{
    [Activity(Label = "Get Location")]
    public class ShowRActivity : Activity, ILocationListener
    {
        static readonly string TAG = "X:" + typeof(ShowRActivity).Name;
        LocationManager locationManager;
        Location clocation;
        string locationProvider;
        TextView addressText;
        TextView locationText;
        double lttd=0;
        double lng=0;

        public async void OnLocationChanged(Location location)
        {
            lttd = location.Latitude;
            lng = location.Longitude;
            addressText.Text = "Latitude: " + lttd;
            locationText.Text = "Longitude: " + lng;
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowR);
            TextView text = FindViewById<TextView>(Resource.Id.textViewR);
            TrainContext trainContext = new TrainContext();
            addressText = FindViewById<TextView>(Resource.Id.mT);

            FindViewById<Button>(Resource.Id.btn_r).Click += AddressButton_OnClick;

            InitializeLocationManager();
            locationText = FindViewById<TextView>(Resource.Id.mT2);
            text.Text = "";
            foreach(Station item in trainContext.GetStationList())
            {
                text.Text += item.ToString() + "\n";
            }
            
        }

        protected override void OnResume()
        {
            base.OnResume();

            string Provider = LocationManager.GpsProvider;

            if (locationManager.IsProviderEnabled(Provider))
            {
                locationManager.RequestLocationUpdates(Provider, 2000, 1, this);
            }
            else
            {
                Log.Info(TAG, Provider + " is not available. Does the device have location services enabled?");
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        async void AddressButton_OnClick(object sender, EventArgs eventArgs)
        {
            TrainContext trainContext = new TrainContext();
            Station[] list = trainContext.GetStationList().ToArray();
            double[,] array = new double[list.Length, 2];
            
            for(int i=0; i < list.Length; i++)
            {
                array[i, 0] = list[i].GetR(lttd, lng);
                array[i, 1] = list[i].Id;
            }
            for(int i = 0; i < list.Length; i++)
            {
                for (int j = 1; j < list.Length; j++)
                {
                    if(array[i, 0] > array[j, 0])
                    {
                        double one, two;
                        one = array[i, 0];
                        two = array[i, 1];
                        array[i, 0] = array[j, 0];
                        array[i, 1] = array[j, 1];
                        array[j, 0] = one;
                        array[j, 1] = two;

                    }
                }
            }
            int id = (int)array[0, 1];
            TextView text = FindViewById<TextView>(Resource.Id.textViewR);
            foreach (Train item in trainContext.GetListId(id))
            {
                text.Text += item.ToString() + "\n";
            }
        }
        
        void InitializeLocationManager()
        {
            locationManager = GetSystemService(Context.LocationService) as LocationManager;
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + locationProvider + ".");
        }






    }
}