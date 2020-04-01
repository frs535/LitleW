using System.Collections.ObjectModel;
using LitleW.Models;
using Plugin.FirebasePushNotification;
using System.Linq;

namespace LitleW.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        #region Constructors

        public NotificationViewModel()
        {
            CrossFirebasePushNotification.Current.OnNotificationReceived += OnNotificationReceived;
            CrossFirebasePushNotification.Current.OnNotificationDeleted += OnNotificationDeleted;
            CrossFirebasePushNotification.Current.OnNotificationOpened += OnNotificationOpened;
        }

        #endregion Constrcutors

        #region Properties

        public ObservableCollection<Notification> Notifications { get; set; } = new ObservableCollection<Notification>();

        private Notification selectedNotification;
        public Notification SelectedNotification
        {
            get => selectedNotification;
            set
            {
                selectedNotification = value;
                OnPropertyChanged("SelectedNotification");

                IsBusy = true;

                

                IsBusy = false;

                selectedNotification = null;
                OnPropertyChanged("SelectedNotification");
            }
        }

        #endregion Properties

        #region Private methods

        private void OnNotificationDeleted(object source, FirebasePushNotificationDataEventArgs e)
        {
         //   string id = e.Data["id"].ToString();
         //   var notif = Notifications.Where(p => p.Id == id).FirstOrDefault();
         //   if (notif is null)
         //       Notifications.Remove(notif);
        }

        private void OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            string id = e.Data["id"].ToString();
            var notif = Notifications.Where(p => p.Id == id).FirstOrDefault();
            if (notif is null)
                Notifications.Add(notif);
        }

        private void OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            string id = e.Data["id"].ToString();

            var n = Notifications.Where(p => p.Id == id).FirstOrDefault();
            if (n != null)
                Notifications.Remove(n);

            var notif = new Notification()
            {
                Id = e.Data["id"].ToString(),
                Type = e.Data["type"].ToString(),
                Title = e.Data["title"].ToString(),
                Body = e.Data["body"].ToString()
            };

            Notifications.Add(notif);
        }

        

        #endregion Private methods
    }
}
