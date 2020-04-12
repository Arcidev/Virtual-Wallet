using SQLite;

namespace DAL.Data
{
    public class Wallet : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        public int? ImageId { get; set; }

        [Ignore]
        public Image Image { get; set; }

        public int TimeRangeId { get; set; }

        public int? CurrencyId { get; set; }
    }
}