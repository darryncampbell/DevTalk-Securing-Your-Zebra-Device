using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace SecurityManagers.Fragments
{
    public class Fragment3 : Fragment
    {
        private IFragmentBridge mainActivity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment3 NewInstance(IFragmentBridge mainActivity)
        {
            var frag3 = new Fragment3 { Arguments = new Bundle() };
            frag3.SetActivity(mainActivity);
            return frag3;
        }

        private void SetActivity(IFragmentBridge fragmentBridge)
        {
            this.mainActivity = fragmentBridge;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.fragment3, null);
            Button buttonDisableWhitelist = view.FindViewById(Resource.Id.btnDisableWhitelist) as Button;
            buttonDisableWhitelist.Click += btnDisableWhitelist_Click;
            Button buttonSimpleWhitelist = view.FindViewById(Resource.Id.btnSimpleWhitelist) as Button;
            buttonSimpleWhitelist.Click += btnSimpleWhitelist_Click;
            Button buttonSignedWhitelist = view.FindViewById(Resource.Id.btnSignedWhitelist) as Button;
            buttonSignedWhitelist.Click += btnSignedWhitelist_Click;
            return view;
        }

        void btnDisableWhitelist_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("AM_Disable_Whitelist");
        }

        void btnSimpleWhitelist_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("AM_Simple_Whitelist");
        }

        void btnSignedWhitelist_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("AM_Signed_Whitelist");
        }
    }
}