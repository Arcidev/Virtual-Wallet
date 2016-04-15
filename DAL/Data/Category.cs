using SQLite.Net.Attributes;

namespace DAL.Data
{
    public class Category : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        public int? IconId { get; set; }

        [Ignore]
        public Icon Icon { get; set; }
    }
}
