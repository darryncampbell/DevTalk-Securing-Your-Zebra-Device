using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace SecurityManagers.Fragments
{
    public class Fragment4 : Fragment
    {
        private IFragmentBridge mainActivity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment4 NewInstance(IFragmentBridge mainActivity)
        {
            var frag4 = new Fragment4 { Arguments = new Bundle() };
            frag4.SetActivity(mainActivity);
            return frag4;
        }

        private void SetActivity(IFragmentBridge fragmentBridge)
        {
            this.mainActivity = fragmentBridge;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.fragment4, null);
            Button buttonEnableCamera = view.FindViewById(Resource.Id.btnEnableCamera) as Button;
            buttonEnableCamera.Click += btnEnableCamera_Click;
            Button buttonDisableCamera = view.FindViewById(Resource.Id.btnDisableCamera) as Button;
            buttonDisableCamera.Click += btnDisableCamera_Click;
            Button buttonInstallCertificate = view.FindViewById(Resource.Id.btnInstallCertificate) as Button;
            buttonInstallCertificate.Click += btnInstallCertificate_Click;
            Button buttonEnableSDCard = view.FindViewById(Resource.Id.btnEnableSDCard) as Button;
            buttonEnableSDCard.Click += btnEnableSDCard_Click;
            Button buttonDisableSDCard = view.FindViewById(Resource.Id.btnDisableSDCard) as Button;
            buttonDisableSDCard.Click += btnDisableSDCard_Click;
            Button buttonEnableUSB = view.FindViewById(Resource.Id.btnEnableUSB) as Button;
            buttonEnableUSB.Click += btnEnableUSB_Click;
            Button buttonDisableUSB = view.FindViewById(Resource.Id.btnDisableUSB) as Button;
            buttonDisableUSB.Click += btnDisableUSB_Click;
            Button buttonEnableAppsFromUnknownSources = view.FindViewById(Resource.Id.btnEnableAppsFromUnknownSources) as Button;
            buttonEnableAppsFromUnknownSources.Click += btnEnableUnknownSources_Click;
            Button buttonDisableAppsFromUnknownSources = view.FindViewById(Resource.Id.btnDisableAppsFromUnknownSources) as Button;
            buttonDisableAppsFromUnknownSources.Click += btnDisableUnknownSources_Click;
            return view;
        }

        void btnEnableCamera_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Enable_Camera");
        }

        void btnDisableCamera_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Disable_Camera");
        }

        void btnInstallCertificate_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Install_Certificate");
        }

        void btnEnableSDCard_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Enable_SDCard");
        }

        void btnDisableSDCard_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Disable_SDCard");
        }

        void btnEnableUSB_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Enable_USB");
        }

        void btnDisableUSB_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Disable_USB");
        }

        void btnEnableUnknownSources_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Enable_Unknown_Sources");
        }

        void btnDisableUnknownSources_Click(object sender, EventArgs e)
        {
            mainActivity.ProcessProfile("OM_Disable_Unknown_Sources");
        }

    }
}