using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Models;
using LitleW.Services;
using Xamarin.Forms;

namespace LitleW.ViewModels
{
    public class GoodViewModel : BaseViewModel
    {
        #region Command

        public ICommand ScanCommand => new Command(async () => await ScanCommandExecute());

        #endregion Command


        public Good Good { get; private set; }

        public GoodViewModel(Good good)
        {
            Good = good;
        }

        private async Task ScanCommandExecute()
        {
            IsBusy = true;

            var scanner = DependencyService.Get<Scaner>();
            var result = await scanner.ScanAsync();
#if DEBUG
            if (string.IsNullOrEmpty(result))
                result = "e5ebfc67-587a-11e6-a74f-001dd8b729fa";
#endif
            IsBusy = false;
        }
    }
}
