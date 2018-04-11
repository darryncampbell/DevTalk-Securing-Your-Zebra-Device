using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.Util;
using System;
using Android.Content.PM;

namespace SecurityManagers.Fragments
{
    public class Fragment1 : Fragment
    {
        private IFragmentBridge mainActivity;
        private TextView statusTextView = null;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment1 NewInstance(IFragmentBridge mainActivity)
        {
            var frag1 = new Fragment1 { Arguments = new Bundle() };
            frag1.SetActivity(mainActivity);
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.fragment1, null);
            statusTextView = view.FindViewById(Resource.Id.textView1) as TextView;
            Button buttonLockDevice = view.FindViewById(Resource.Id.btnLockDevice) as Button;
            buttonLockDevice.Click += btnLockDevice_Click;
            Button buttonRevokeKeys = view.FindViewById(Resource.Id.btnRevokeKeys) as Button;
            buttonRevokeKeys.Click += btnRevokeKeys_Click;
            Button buttonUnsolicitedAlert = view.FindViewById(Resource.Id.btnUnsolicitedAlert) as Button;
            buttonUnsolicitedAlert.Click += btnUnsolicitedAlert_Click;
            return view;
        }

        void btnLockDevice_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("TM_Lock_Device");
        }

        void btnRevokeKeys_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("TM_Revoke_Keys");
        }

        void btnUnsolicitedAlert_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("TM_Unsolicited_Alert");
        }

        private void SetActivity(IFragmentBridge fragmentBridge)
        {
            this.mainActivity = fragmentBridge;
        }

    }
}