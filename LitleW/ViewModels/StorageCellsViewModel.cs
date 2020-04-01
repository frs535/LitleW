using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Helpers;
using LitleW.Models;
using LitleW.Services;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;

namespace LitleW.ViewModels
{
    public class StorageCellsViewModel : BaseViewModel
    {
        #region Constructors

        public StorageCellsViewModel()
        {
            CrossFirebasePushNotification.Current.OnNotificationOpened += OnNotificationOpened;
        }

        #endregion Constructors

        #region Command

        public ICommand ScanCommand => new Command(async () => await ScanCommandExecute());

        #endregion Command

        #region Properties

        private ObservableCollection<StorageCells> storagesCells = new ObservableCollection<StorageCells>();
        public ObservableCollection<StorageCells> StoragesCells
        {
            get => storagesCells;
            set
            {
                storagesCells = value;
                OnPropertyChanged("StoragesCells");
            }
        }

        private StorageCells selectedStorageCells;
        public StorageCells SelectedStorageCells
        {
            get => selectedStorageCells;
            set
            {
                selectedStorageCells = value;
                OnPropertyChanged("SelectedStorageCells");

                Task.Run(() => MVVMHelper.OpenView(value));

                selectedStorageCells = null;
                OnPropertyChanged("SelectedStorageCells");
            }
        }

        #endregion Protperties

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

            if (!string.IsNullOrEmpty(result))
                await LoadDocument(result);

            IsBusy = false;
        }

        private void OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            if (!GlobalSetting.Instance.IsAuthenticated)
                return;

            if (!e.Data.ContainsKey("type") && e.Data["type"].ToString() != "StorageCells")
                return;

            IsBusy = true;
            LoadDocument(e.Data["id"].ToString());
            IsBusy = false;
        }

        private async Task LoadDocument(string id)
        {
            var service = DependencyService.Get<Service>();
            var storageCells = await service.GetDocumentById<StorageCells>(id);
            if (storageCells != null)
            {
                var finedStorageSelles = StoragesCells.Where(p => p.Id == storageCells.Id).FirstOrDefault();
                if (finedStorageSelles != null)
                    StoragesCells.Remove(finedStorageSelles);

                StoragesCells.Add(storageCells);

                await MVVMHelper.OpenView(storageCells);
            }
        }

        #endregion Private methods
    }
}
