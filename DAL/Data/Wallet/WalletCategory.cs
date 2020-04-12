using SQLite;

namespace DAL.Data
{
    public class WalletCategory : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public int WalletId { get; set; }

        [Ignore]
        public Wallet Wallet { get; set; }
        
        public int CategoryId { get; set; }

        [Ignore]
        public Category Category { get; set; }
    }
}
