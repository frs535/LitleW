using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LitleW.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        public ICommand TakePhotoCommand => new Command(async () => await TakePhotoCommandExecute());

        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        private async Task TakePhotoCommandExecute()
        {
            
        }
    }
}
