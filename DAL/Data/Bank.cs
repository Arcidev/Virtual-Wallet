using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace DAL.Data
{
    public class Bank : IDao
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        public int? ImageId { get; set; }

        [Ignore]
        public Image Image { get; set; }

        [Ignore]
        public BankAccountInfo BankAccountInfo { get; set; }

        [Ignore]
        public IList<Transaction> StoredTransactions { get; set; }
    }
}
