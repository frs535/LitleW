using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Models;
using LitleW.Models.Base;
using LitleW.Services;
using Xamarin.Forms;
using System.Linq;
using LitleW.Helpers;

namespace LitleW.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        #region Command

        public ICommand ScanCommand => new Command(async () => await ScanCommandExecute());
        public ICommand DoneCommand => new Command(async () =>  await DoneCommandExecute());

        #endregion Command

        public Order Order
        {
            get;
            private set;
        }

        public OrderDetailViewModel(Order order)
        {
            Order = order;
        }

        private Good currentRow;
        public Good CurrentRow
        {
            get => currentRow;
            set
            {
                currentRow = value;
                OnPropertyChanged("CurrentRow");

                MVVMHelper.OpenView(value);

                currentRow = null;
                OnPropertyChanged("CurrentRow");
            }
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
            var item = GlobalParameters.GetItemByBarcode(result);
            if (item is null)
                return;

            var good = Order.Goods.Where(p => p.Catalog == item).FirstOrDefault();
            if (good is null)
                return;

            await MVVMHelper.OpenView(good);

            IsBusy = false;
        }

        private async Task DoneCommandExecute()
        {
            IsBusy = true;

            var service = DependencyService.Get<Service>();
            await service.UpdateDocument(Order);

            await MVVMHelper.PopAsync();

            IsBusy = false;
        }
    }
}