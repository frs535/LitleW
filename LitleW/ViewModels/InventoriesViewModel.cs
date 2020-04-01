using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Helpers;
using LitleW.Models;
using LitleW.Services;
using Xamarin.Forms;
using System.Linq;
using Plugin.FirebasePushNotification;

namespace LitleW.ViewModels
{
    public class InventoriesViewModel : BaseViewModel
    {
        #region Constructors

        public InventoriesViewModel()
        {
            CrossFirebasePushNotification.Current.OnNotificationOpened += OnNotificationOpened;
        }

        #endregion Constructors

        #region Command

        public ICommand ScanCommand => new Command(async () => await ScanCommandExecute());

        #endregion Command

        #region Properties

        private ObservableCollection<Inventory> inventories = new ObservableCollection<Inventory>();
        public ObservableCollection<Inventory> Inventories
        {
            get => inventories;
            set
            {
                inventories = value;
                OnPropertyChanged("Inventories");
            }
        }

        private Inventory selectedInventory;
        public Inventory SelectedInventory
        {
            get => selectedInventory;
            set
            {
                selectedInventory = value;
                OnPropertyChanged("SelectedInventory");

                Task.Run(() => MVVMHelper.OpenView(value));

                selectedInventory = null;
                OnPropertyChanged("SelectedInventory");
            }
        }

        #endregion Properties

        #region Private methods

        private void OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            if (!GlobalSetting.Instance.IsAuthenticated)
                return;

            if (!e.Data.ContainsKey("type") && e.Data["type"].ToString() != "Inventory")
                return;

            IsBusy = true;
            LoadDocument(e.Data["id"].ToString());
            IsBusy = false;
        }

        private async Task ScanCommandExecute()
        {
            IsBusy = true;

            var scanner = DependencyService.Get<Scaner>();
            var result = await scanner.ScanAsync();

            if (!string.IsNullOrEmpty(result))
                await LoadDocument(result);

                IsBusy = false;
        }

        private async Task LoadDocument(string id)
        {
            var service = DependencyService.Get<Service>();
            var inventory = await service.GetDocumentById<Inventory>(id);
            if (inventory != null)
            {
                var finedOrder = Inventories.Where(p => p.Id == inventory.Id).FirstOrDefault();
                if (finedOrder != null)
                    Inventories.Remove(finedOrder);

                Inventories.Add(inventory);

                await MVVMHelper.OpenView(inventory);
            }
        }

        #endregion Private methods
    }
}
