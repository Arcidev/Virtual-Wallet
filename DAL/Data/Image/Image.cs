using SQLite;

namespace DAL.Data
{
    public class Image : IDao
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NotNull, MaxLength(50)]
        public string Path { get; set; }
    }
}
