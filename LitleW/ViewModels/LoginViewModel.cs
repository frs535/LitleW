using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Helpers;
using LitleW.Services;
using Xamarin.Forms;

namespace LitleW.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Commands

        public ICommand LoginCommand => new Command(async () => await LoginCommandExecute());
        public ICommand ActivateCommand => new Command(MVVMHelper.RegisterPage);
        public ICommand ScanCommand => new Command(async () => await ScanCommandExecute());

        #endregion Commands

        public LoginViewModel()
        {
            

#if DEBUG
            login = GlobalSetting.Instance.Login;
            password = GlobalSetting.Instance.Password;
#endif
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

        

        #endregion Properties

        #region Private methods

        private async Task LoginCommandExecute()
        {
            if (string.IsNullOrEmpty(Login))
                throw new ArgumentNullException("Loпin is empty");

            IsBusy = true;

            var service = DependencyService.Get<IService>();            
            var result = await service.Login(Login, Password);

            if (result)
                MVVMHelper.WorkSpacePage();

            IsBusy = false;
        }

        private async Task ScanCommandExecute()
        {
            IsBusy = true;

            var scanner = DependencyService.Get<Scaner>();
            var result = await scanner.ScanAsync();

            IsBusy = false;
        }

        #endregion Private methods
    }
}
