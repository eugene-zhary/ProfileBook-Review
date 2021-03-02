using SQLite;

namespace ProfileBook.Models
{
    [Table("Settings")]
    public class Settings : IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Enums.SortType Sort { get; set; }
        public Enums.Theme Theme { get; set; }
        public Enums.Language Language { get; set; }

        public int UserId { get; set; }
    }
}
