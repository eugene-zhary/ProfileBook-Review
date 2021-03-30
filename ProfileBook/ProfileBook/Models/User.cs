using SQLite;

namespace ProfileBook.Models
{
    [Table("Users")]
    public class User : IEntityModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
