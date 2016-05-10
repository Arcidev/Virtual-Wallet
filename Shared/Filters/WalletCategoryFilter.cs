
namespace Shared.Filters
{
    public class WalletCategoryFilter : BaseFilter
    {
        public int? WalletId { get; set; }

        public int? CategoryId { get; set; }
    }
}
