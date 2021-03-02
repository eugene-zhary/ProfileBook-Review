using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.ViewModels.Dialogs
{
    public class PickImageDialogViewModel : BindableBase, IDialogAware
    {
        public string CameraImagePath { get; set; }
        public string GalleryImagePath { get; set; }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        /// <summary>
        /// set the icons according current theme
        /// </summary>
        private void setImgPath()
        {
            switch (App.CurrentSettings.Theme) {
                case Enums.Theme.Dark:
                    CameraImagePath = "ic_camera_alt.png";
                    GalleryImagePath = "ic_collections.png";
                    break;
                default:
                case Enums.Theme.Light:
                    CameraImagePath = "ic_camera_alt_black.png";
                    GalleryImagePath = "ic_collections_black.png";
                    break;
            }
        }

        public void OnDialogOpened(IDialogParameters parameters) { }

        public void OnDialogClosed() { }

        public PickImageDialogViewModel()
        {
            setImgPath();
            CameraCommand = new Command(executeCamera);
            GalleryCommand = new Command(executeGallery);
        }

        public ICommand CameraCommand { get; }

        /// <summary>
        /// pick the photo from the camera
        /// </summary>
        private async void executeCamera()
        {
            try {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions {
                    Title = $"photo.{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.png"
                });
                await SaveAndReturn(photo);
            }
            catch (Exception) { }
        }

        public ICommand GalleryCommand { get; }

        /// <summary>
        /// pick the photo from gallery
        /// </summary>
        private async void executeGallery()
        {
            try {
                var photo = await MediaPicker.PickPhotoAsync();
                await SaveAndReturn(photo);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// save image to local storage and return path of the image
        /// </summary>
        private async Task SaveAndReturn(FileResult photo)
        {
            //save to local storage
            var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            //return image path
            var dialog_params = new DialogParameters {
                { "ImagePath", photo.FullPath }
            };
            RequestClose?.Invoke(dialog_params);
        }
    }
}
