
using SQLite.Net.Attributes;

namespace DAL.Data
{
    public class BankAccountInfo : IDao
    {
        [PrimaryKey]
        public int Id { get; set; }

        public double ClosingBalance { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; }

        public double OpeningBalance { get; set; }

        [MaxLength(20)]
        public string DateStart { get; set; }

        [MaxLength(20)]
        public string DateEnd { get; set; }
    }
}
