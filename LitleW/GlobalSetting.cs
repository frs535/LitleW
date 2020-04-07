using LitleW.Helpers;
using LitleW.Services;

namespace LitleW
{
    public class GlobalSetting
    {
        private readonly IRequestProvider _requestProvider;

        public GlobalSetting()
        {
            _requestProvider = new RequestProvider();

            baseName = GetAppSetting("baseName", string.Empty).ToString();
            serverName = GetAppSetting("serverName", string.Empty).ToString();
#if DEBUG
            login = GetAppSetting("loginDebug", string.Empty).ToString();
            passwod = GetAppSetting("passwodDebug", string.Empty).ToString();
#endif
        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public IRequestProvider RequestProvider
        {
            get { return _requestProvider; }
        }

        public bool IsAuthenticated { get; set; }

        public string ServiceEndPoint
        {
            get
            {
                if (string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(baseName))
                    return string.Empty;

                return $"https://{serverName}/{baseName}/hs/Warehouse";
            }
        }

        private string baseName = string.Empty;
        public string BaseName
        {
            get => baseName;
            set
            {
                baseName = value;
                UpdateAppSetting("baseName", value);
            }
        }

        private string serverName = string.Empty;
        public string ServerName
        {
            get => serverName;
            set
            {
                serverName = value;
                UpdateAppSetting("serverName", value);
            }
        }

        private string login = string.Empty;
        public string Login
        {
            get => login;
            set
            {
                login = value;
#if DEBUG
                UpdateAppSetting("loginDebug", login);
#endif
            }
        }

        private string passwod = string.Empty;
        public string Password
        {
            get => passwod;
            set
            {
                passwod = value;
#if DEBUG
                UpdateAppSetting("passwodDebug", passwod);
#endif
            }
        }

        public bool UseSeries { get; set; }

        public bool UseSpecifications { get; set; }

        private void UpdateAppSetting(string key, object value)
        {
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey(key))
                Xamarin.Forms.Application.Current.Properties[key] = value;
            else
                Xamarin.Forms.Application.Current.Properties.Add(key, value);
        }

        private object GetAppSetting(string key, object defaultValue)
        {
            object value;
            Xamarin.Forms.Application.Current.Properties.TryGetValue(key, out value);

            if (value is null)
                return defaultValue;

            return value;
        }
    }
}
