namespace BL.Models
{
    public class WalletBank : IDto
    {
        public int Id { get; set; }

        public int WalletId { get; set; }
        
        public Wallet Wallet { get; set; }

        public int BankId { get; set; }
        
        public Bank Bank { get; set; }
    }
}
