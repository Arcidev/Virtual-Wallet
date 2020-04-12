using SQLite;
using System;

namespace DAL.Data
{
    public class Transaction : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name = "BankTransactionIndex", Order = 2, Unique = true)]
        public long? ExternalId { get; set; }

        [Indexed(Name = "BankTransactionIndex", Order = 1, Unique = true)]
        public int? BankId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; }
    }
}
