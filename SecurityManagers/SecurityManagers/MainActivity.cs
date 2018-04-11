using System;
using System.Xml;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;

using SecurityManagers.Fragments;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

using Symbol.XamarinEMDK;
using System.IO;
using Android.Widget;

namespace SecurityManagers
{
    [Activity(Label = "@string/app_name", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    public class MainActivity : AppCompatActivity, EMDKManager.IEMDKListener, IFragmentBridge
    {

        DrawerLayout drawerLayout;
        NavigationView navigationView;
        private EMDKManager emdkManager = null;
        private ProfileManager profileManager = null;


        IMenuItem previousItem;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                if (previousItem != null)
                    previousItem.SetChecked(false);

                navigationView.SetCheckedItem(e.MenuItem.ItemId);

                previousItem = e.MenuItem;

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home_1:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_home_2:
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_home_3:
                        ListItemClicked(2);
                        break;
                    case Resource.Id.nav_home_4:
                        ListItemClicked(3);
                        break;
                }


                drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                navigationView.SetCheckedItem(Resource.Id.nav_home_1);
                ListItemClicked(0);
            }

            // The EMDKManager object will be created and returned in the callback
            EMDKResults results = EMDKManager.GetEMDKManager(Application.Context, this);

            // Check the return status of processProfile
            if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            {
                // EMDKManager object initialization failed
                output("EMDKManager object creation failed ...");
            }
            else
            {
                // EMDKManager object initialization succeeded
                output("EMDKManager object creation succeeded ...");
            }

            try
            {
                System.Collections.Generic.IList<Signature> sigs = Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, PackageInfoFlags.Signatures).Signatures;
                foreach (Signature sig in sigs)
                {
                    //  Care: App signature may exceed allowed output length
                    Console.WriteLine("Current app: " + sig.ToCharsString());
                    String signatureString = sig.ToCharsString();
                }
                
                System.Collections.Generic.IList<Signature> sigs2 = Application.Context.ApplicationContext.PackageManager.GetPackageInfo("com.darryn.emmclientstub", PackageInfoFlags.Signatures).Signatures;
                foreach (Signature sig in sigs2)
                {
                    Console.WriteLine("EMM Stub App: " + sig.ToCharsString());
                    String signatureString = sig.ToCharsString();
                }
                
            }
            catch (Exception e) { }

        }

        int oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;
            Android.Support.V4.App.Fragment fragment = null;

            switch (position)
            {
                case 0:
                    fragment = Fragment1.NewInstance(this);
                    break;
                case 1:
                    fragment = Fragment2.NewInstance(this);
                    break;
                case 2:
                    fragment = Fragment3.NewInstance(this);
                    break;
                case 3:
                    fragment = Fragment4.NewInstance(this);
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
        
            // Clean up the objects created by EMDK manager
            if (profileManager != null)
            {
                profileManager = null;
            }

            if (emdkManager != null)
            {
                emdkManager.Release();
                emdkManager = null;
            }
        }

        public void OnClosed()
        {
            if (emdkManager != null)
            {
                emdkManager.Release();
                emdkManager = null;
            }

            output("EMDK closed unexpectedly! Please close and restart the application.");
        }

        public void OnOpened(EMDKManager emdkManagerInstance)
        {
            this.emdkManager = emdkManagerInstance;

            try
            {
                // Get the ProfileManager object to process the profiles
                profileManager = (ProfileManager)emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Profile);

                // Add listener to get async results
                profileManager.Data += profileManager_Data;
            }
            catch (Exception e)
            {
                output("Error loading profile manager.");
                Console.WriteLine("Exception: " + e.StackTrace);
            }

        }

        void profileManager_Data(object sender, ProfileManager.DataEventArgs e)
        {
            // Call back with the result of the processProfileAsync
            EMDKResults results = e.P0.Result;

            string statusString = CheckXmlError(results);
            output(statusString);
            
        }

        private string CheckXmlError(EMDKResults results)
        {
            StringReader stringReader = null;
            string checkXmlStatus = "";
            bool isFailure = false;

            try
            {
                if (results.StatusCode == EMDKResults.STATUS_CODE.CheckXml)
                {
                    stringReader = new StringReader(results.StatusString);

                    using (XmlReader reader = XmlReader.Create(stringReader))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                switch (reader.Name)
                                {
                                    case "parm-error":
                                        isFailure = true;
                                        string parmName = reader.GetAttribute("name");
                                        string parmErrorDescription = reader.GetAttribute("desc");
                                        checkXmlStatus = "Name: " + parmName + ", Error Description: " + parmErrorDescription;
                                        break;
                                    case "characteristic-error":
                                        isFailure = true;
                                        string errorType = reader.GetAttribute("type");
                                        string charErrorDescription = reader.GetAttribute("desc");
                                        checkXmlStatus = "Type: " + errorType + ", Error Description: " + charErrorDescription;
                                        break;
                                }
                            }
                        }
                        if (!isFailure)
                        {
                            checkXmlStatus = "Profile applied successfully ...";
                        }
                    }
                }
            }
            finally
            {
                if (stringReader != null)
                {
                    stringReader.Dispose();
                }
            }
            return checkXmlStatus;
        }

        private void output(String outputString)
        {
            Console.WriteLine(outputString);
            Toast.MakeText(this, outputString, ToastLength.Short).Show();
        }

        public void ProcessProfile(String profileName)
        {
            Console.WriteLine("Testing");

            string[] modifyData1 = new string[1];
            
            // Call processPrfoileAsync with profile name, 'Set' flag and modify data to update the profile
            EMDKResults results = profileManager.ProcessProfileAsync(profileName, ProfileManager.PROFILE_FLAG.Set, modifyData1);

            string resultString = results.StatusCode == EMDKResults.STATUS_CODE.Processing ? "Set profile in-progress..." : "Set profile failed.";
            output(resultString);
            
        }
    }
}

