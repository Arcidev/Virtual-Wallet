
namespace Shared.Filters
{
    public class CurrencyFilter : BaseFilter
    {
        public string Code { get; set; }

        public bool IsDefaultCurrency { get; set; }
    }
}
