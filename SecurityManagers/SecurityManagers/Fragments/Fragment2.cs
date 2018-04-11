using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace SecurityManagers.Fragments
{
    public class Fragment2 : Fragment
    {
        private IFragmentBridge mainActivity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment2 NewInstance(IFragmentBridge mainActivity)
        {
            var frag2 = new Fragment2 { Arguments = new Bundle() };
            frag2.SetActivity(mainActivity);
            return frag2;
        }

        private void SetActivity(IFragmentBridge fragmentBridge)
        {
            this.mainActivity = fragmentBridge;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.fragment2, null);
            Button buttonEncryptSDCard = view.FindViewById(Resource.Id.btnEncryptSDCard) as Button;
            buttonEncryptSDCard.Click += btnEncryptSDCard_Click;
            Button buttonInstallKey = view.FindViewById(Resource.Id.btnInstallKey) as Button;
            buttonInstallKey.Click += btnInstallKey_Click;
            Button buttonRevokeKey = view.FindViewById(Resource.Id.btnRevokeKey) as Button;
            buttonRevokeKey.Click += btnRevokeKey_Click;
            return view;
        }

        void btnEncryptSDCard_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("EM_Encrypt_Storage_Card");
        }

        void btnInstallKey_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("EM_Install_Key");
        }

        void btnRevokeKey_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("EM_Revoke_Key");
        }

    }
}