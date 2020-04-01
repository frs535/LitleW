using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Helpers;
using LitleW.Models;
using LitleW.Services;
using Xamarin.Forms;

namespace LitleW.ViewModels
{
    public class StorageCellsDetailViewModel : BaseViewModel
    {
        #region Command

        public ICommand ScanCommand => new Command(async () => await ScanCommandExecute());
        public ICommand DoneCommand => new Command(async () => await DoneCommandExecute());

        #endregion Command

        #region Properties

        public StorageCells StorageCells { get; private set; }

        private Good currentRow;
        public Good CurrentRow
        {
            get => currentRow;
            set
            {
                currentRow = value;
                OnPropertyChanged("CurrentRow");

                IsBusy = true;

                MVVMHelper.OpenView(value);
                //Task.Run(() => MVVMHelper.OpenView(value));

                IsBusy = false;

                currentRow = null;
                OnPropertyChanged("CurrentRow");
            }
        }

        private ObservableCollection<Good> goods;
        public ObservableCollection<Good> Goods
        {
            get => goods;
            private set
            {
                goods = value;
                OnPropertyChanged("Goods");
            }
        } 

        #endregion Properties

        #region Constructors

        public StorageCellsDetailViewModel(StorageCells storageCells)
        {
            StorageCells = storageCells;

            if (StorageCells.Type == "placing")
                Goods = StorageCells.GoodsPlacing;
            else if (StorageCells.Type == "selection")
                Goods = StorageCells.GoodsSelection;
            else if (StorageCells.Type == "moving")
                Goods = StorageCells.GoodsSelection;

        }
        #endregion Constructors

        #region Private methods

        private async Task ScanCommandExecute()
        {
            IsBusy = true;

            var scanner = DependencyService.Get<Scaner>();
            var result = await scanner.ScanAsync();
#if DEBUG
            if (string.IsNullOrEmpty(result))
                result = "ce0fa819-12b4-11ea-8436-9c5c8ebce33c";
#endif
            var item = GlobalParameters.GetItemByBarcode(result);
            if (item is null)
                return;

            var good = StorageCells.GoodsPlacing.Where(p => p.Catalog == item).FirstOrDefault();
            if (good is null)
                return;

            await MVVMHelper.OpenView(good);

            IsBusy = false;
        }

        private async Task DoneCommandExecute()
        {
            IsBusy = true;

            var service = DependencyService.Get<Service>();
            await service.UpdateDocument(StorageCells);

            await MVVMHelper.PopAsync();

            IsBusy = false;
        }

        #endregion Private methods
    }
}

