
namespace Shared.Filters
{
    public class WalletBankFilter : BaseFilter
    {
        public int? WalletId { get; set; }

        public int? BankId { get; set; }
    }
}
