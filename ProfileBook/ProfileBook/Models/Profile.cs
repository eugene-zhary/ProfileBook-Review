using SQLite;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class Profile : IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Image { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        
        public Profile()
        {
            this.Image = "pic_profile.png";
            this.UserId = App.CurrentUser.Id;
            this.CreationDate = DateTime.Now;
        }

        public event EventHandler EditProfile;
        public event EventHandler RemoveProfile;
        public event EventHandler ShowImage;

        public ICommand RemoveProfileCommand => new Command(() => {
            RemoveProfile?.Invoke(this, new EventArgs());
        });
        public ICommand EditProfileCommand => new Command(() => {
            EditProfile?.Invoke(this, new EventArgs());
        });
        public ICommand ShowImageCommand => new Command(() => {
            ShowImage?.Invoke(this, new EventArgs());
        });
    }
}