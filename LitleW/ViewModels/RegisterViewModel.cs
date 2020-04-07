
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Helpers;
using LitleW.Services;
using Xamarin.Forms;

namespace LitleW.ViewModels
{
    public class RegisterViewModel:BaseViewModel
    {
        #region Commands

        public ICommand RegisterCommand => new Command(async () => await RegisterCommandExcute());

        #endregion Commands

        public RegisterViewModel()
        {
            baseName = GlobalSetting.Instance.BaseName;
            serverName = GlobalSetting.Instance.ServerName;
        }

        #region Properties

        private string login = string.Empty;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        private string password = string.Empty;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string baseName = string.Empty;
        public string BaseName
        {
            get => baseName;
            set
            {
                baseName = value;

                OnPropertyChanged("BaseName");
                OnPropertyChanged("ServiceEndPoint");
            }
        }

        private string serverName = string.Empty;
        public string ServerName
        {
            get => serverName;
            set
            {
                serverName = value;

                OnPropertyChanged("BaseName");
                OnPropertyChanged("ServiceEndPoint");
            }
        }

        public string ServiceEndPoint
        {
            get
            {
                if (string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(baseName))
                    return string.Empty;

                return $"https://{serverName}/{baseName}/hs/Warehouse";
            }
        }

        #endregion Properties

        private async Task RegisterCommandExcute()
        {
            if (string.IsNullOrEmpty(Login))
                throw new ArgumentNullException("Loпin is empty");

            IsBusy = true;

            var service = DependencyService.Get<IService>();
            var result = await service.Login(serverName, baseName, login, password);

            if (result)
                MVVMHelper.WorkSpacePage();

            IsBusy = false;
        }
    }
}
