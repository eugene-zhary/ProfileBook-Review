using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.Dialogs
{
    public class ShowImageDialogViewModel : BindableBase, IDialogAware
    {
        private ImageSource image;
        public ImageSource Image {
            get => image;
            set => SetProperty(ref image, value, nameof(Image));
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Image = parameters.GetValue<ImageSource>("Image");
        }

        public ShowImageDialogViewModel() { }

        public ICommand CloseCommand => new Command(() => {
            try {
                RequestClose(null);
            }
            catch (NullReferenceException) {

            }
        });
    }
}
