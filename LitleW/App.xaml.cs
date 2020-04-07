using System.Threading.Tasks;
using LitleW.Helpers;
using LitleW.Models;
using LitleW.Services;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;

namespace LitleW
{
    public partial class App : Application
    {
        public App()
        {
            
            InitializeComponent();

            DependencyService.Register<Service>();
            DependencyService.Register<Scaner>();

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };

            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }
                }
            };
        }

        protected override void OnStart()
        {
            if (GlobalSetting.Instance.IsAuthenticated)
                MVVMHelper.WorkSpacePage();
            else
                MVVMHelper.LoginPage();

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {

        }
    }
}
