using System;
using Android.App;
using Android.OS;
using Firebase;
using Plugin.FirebasePushNotification;

namespace LitleW.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, Android.Runtime.JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {

            };

            var options = new FirebaseOptions.Builder()
                  .SetApplicationId("1:631962306174:android:bee68b9aaa965ed726ba2f")
                  .SetApiKey("AIzaSyDkCDZzVNNUuNBuLI5Dk7tKRTRH7u30TIo").Build();
                  //.SetDatabaseUrl("https://litlew.firebaseio.com")
                  //.SetStorageBucket("litlew.appspot.com")
                  //.SetGcmSenderId("631962306174-jg3p2gfpdgto3e8f4ih707rnistpjfr3.apps.googleusercontent.com").Build();
            var fapp = FirebaseApp.InitializeApp(this, options);

            
            //FirebaseOptions.

            //FirebaseApp.InitializeApp(this);

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }


            //If debug you should reset the token each time.
#if DEBUG
            FirebasePushNotificationManager.Initialize(this, true);
#else
              FirebasePushNotificationManager.Initialize(this,false);
#endif
            
            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {


            };


        }
    }
}
