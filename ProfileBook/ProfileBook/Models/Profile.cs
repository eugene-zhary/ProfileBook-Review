using SQLite;
using System;
using Xamarin.Essentials;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class Profile : IEntityModel
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
            this.UserId = Preferences.Get($"{nameof(UserId)}", 0);
            this.CreationDate = DateTime.Now;
        }
    }
}