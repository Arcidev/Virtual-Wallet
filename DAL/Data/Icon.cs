using SQLite.Net.Attributes;

namespace DAL.Data
{
    public class Icon : IDao
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NotNull, MaxLength(50)]
        public string Path { get; set; }
    }
}
