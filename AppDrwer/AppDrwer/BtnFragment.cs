
using Android.App;
using Android.OS;
using Android.Views;

namespace AppDrwer
{
    public class BtnFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.btnFragment, container, false);
            return view;
        }
    }
}