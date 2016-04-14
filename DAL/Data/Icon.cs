using SQLite.Net.Attributes;

namespace DAL.Data
{
    public class Icon : IData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        [NotNull, MaxLength(50)]
        public string Path { get; set; }
    }
}
