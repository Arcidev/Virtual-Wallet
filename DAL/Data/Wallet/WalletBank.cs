using SQLite;

namespace DAL.Data
{
    public class WalletBank : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public int WalletId { get; set; }

        [Ignore]
        public Wallet Wallet { get; set; }
        
        public int BankId { get; set; }

        [Ignore]
        public Bank Bank { get; set; }
    }
}
