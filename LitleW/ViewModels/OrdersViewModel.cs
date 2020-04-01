using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using LitleW.Models;
using LitleW.Services;
using Xamarin.Forms;
using Plugin.FirebasePushNotification;
using System.Linq;
using LitleW.Helpers;

namespace LitleW.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        #region Constructors

        public OrdersViewModel()
        {
            CrossFirebasePushNotification.Current.OnNotificationOpened += OnNotificationOpened;
        }

        #endregion Constructors

        #region Command

        public ICommand ScanCommand => new Command(async () => await ScanCommandExecute());

        #endregion Command

        #region Properties

        private ObservableCollection<Order> orderList = new ObservableCollection<Order>();
        public ObservableCollection<Order> OrderList
        {
            get => orderList;
            set
            {
                orderList = value;
                OnPropertyChanged("OrderList");
            }
        }

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            { 
                IsBusy = true;

                Task.Run(() => MVVMHelper.OpenView(value));

                IsBusy = false;

                selectedOrder = null;
            
                OnPropertyChanged("SelectedOrder");
            }
        }

        #endregion Protperties

        #region Private methods

        private async Task ScanCommandExecute()
        {
            IsBusy = true;

            var scanner = DependencyService.Get<Scaner>();
            var result = await scanner.ScanAsync();

            if (string.IsNullOrEmpty(result))
                await LoadDocument(result);

            IsBusy = false;
        }

        private void OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            if (!GlobalSetting.Instance.IsAuthenticated)
                return;

            if (!e.Data.ContainsKey("type") && e.Data["type"].ToString() != "Order")
                return;

            IsBusy = true;
            LoadDocument(e.Data["id"].ToString());
            IsBusy = false;
        }

        private async Task LoadDocument(string id)
        {
            var service = DependencyService.Get<Service>();
            var order = await service.GetDocumentById<Order>(id);
            if (order != null)
            {
                var finedOrder = OrderList.Where(p => p.Id == order.Id).FirstOrDefault();
                if (finedOrder != null)
                    OrderList.Remove(finedOrder);

                OrderList.Add(order);

                await MVVMHelper.OpenView(order);
            }
        }

        #endregion Private methods
    }
}
