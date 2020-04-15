using SQLite;
using System.Collections.Generic;

namespace DAL.Data
{
    public class Category : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        public int? ImageId { get; set; }

        [Ignore]
        public Image Image { get; set; }

        [Ignore]
        public List<Wallet> Wallets { get; set; }

        [Ignore]
        public List<Rule> Rules { get; set; }
    }
}
