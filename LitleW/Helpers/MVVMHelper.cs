using System.Threading.Tasks;
using LitleW.Models;
using LitleW.ViewModels;
using LitleW.Views;
using Xamarin.Forms;

namespace LitleW.Helpers
{
    public static class MVVMHelper
    {

        public static async Task OpenView(Order order)
        {
            var page = new OrderDetailView() { BindingContext = new OrderDetailViewModel(order) };
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public static async Task OpenView(Good good)
        {
            var page = new GoodView() { BindingContext = new GoodViewModel(good) };
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public static async Task OpenView(Inventory inventory)
        {
            var page = new InventoryDetailView() { BindingContext = new InventoryDetailViewModel(inventory) };
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public static async Task OpenView(StorageCells storageCells)
        {
            var page = new StorageCellsDetailView() { BindingContext = new StorageCellsDetailViewModel(storageCells) };
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public static async void LoginPage()
        {
            if (App.Current.MainPage == null)
            {
                App.Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var page = new LoginView();
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public static async void RegisterPage()
        {
            var page = new RegisterView();
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public static async Task PopAsync()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public static async void WorkSpacePage()
        {
            var page = new WorkSpaceView();
            
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

    }
}
