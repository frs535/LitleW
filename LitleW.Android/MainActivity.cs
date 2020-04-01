using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LitleW.Droid.Scaner;
using Symbol.XamarinEMDK.Barcode;
using Plugin.FirebasePushNotification;
using Android.Content;

namespace LitleW.Droid
{
    [Activity(Label = "LitleW", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity//, View.IOnClickListener
    {
        //Scaner
        IBarcodeScannerManager _scannerManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            #region Scaner
            //Scaner
            //SetContentView(Resource.Layout.activity_main);

            //Scaner
            //try
            //{
            //    _scannerManager = new BarcodeScannerManager(this);
            //    _scannerManager.ScanReceived += OnScanReceived;
            //}
            //catch (SystemException e)
            //{

            //}

            //Scaner
            //var toggleScannerButton = FindViewById<Button>(Resource.Id.toggle_scanner_button);
            //toggleScannerButton.SetOnClickListener(this);

            #endregion Scaner

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            #region Notification

            FirebasePushNotificationManager.ProcessIntent(this, base.Intent);
            //string var = CrossFirebasePushNotification.Current.Token;

            #endregion Notification

        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }

        void OnScanReceived(object sender, Scanner.DataEventArgs args)
        {
            var scanDataCollection = args.P0;
            if (scanDataCollection?.Result == ScannerResults.Success)
            {
                //var textView = FindViewById<TextView>(Resource.Id.last_scan_received);
                //if (textView != null)
                //    textView.Text = scanDataCollection.GetScanData()[0].Data;
            }
        }

        public void OnClick(View v)
        {
            if (_scannerManager.IsScannerEnabled)
                _scannerManager.Disable();//DisableScanner
            else
                _scannerManager.Enable();//EnableScanner
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            //Scaner
            //_scannerManager.ScanReceived -= OnScanReceived;
            //_scannerManager.Dispose();
            //_scannerManager = null;
        }


    }
}