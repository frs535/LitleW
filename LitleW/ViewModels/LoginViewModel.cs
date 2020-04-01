using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Helpers;
using LitleW.Services;
using LitleW.Views;
using Xamarin.Forms;

namespace LitleW.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Commands

        public ICommand LoginCommand => new Command(async () => await LoginCommandExecute());

        #endregion Commands

        public LoginViewModel()
        {
#if DEBUG
            login = "exchange";
            password = "111396";
#endif
        }

        #region Properties

        private string login = string.Empty;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        private string password = string.Empty;
        public string Password
        {
            get { return password; }
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
                throw new ArgumentNullException("Loin is empty");

            IsBusy = true;

            var service = DependencyService.Get<IService>();            
            var result = await service.Login(Login, Password);

            if (result)
                MVVMHelper.WorkSpacePage();

            IsBusy = false;
        }

        #endregion Private methods
    }
}
