
namespace Shared.Formatters
{
    public static class CurrencyFormatter
    {
        public static string GetFormatter(string currency)
        {
            return $"{{0:# ###.##}} {currency}";
        }

        public static string Format(double value, string currency)
        {
            return string.Format(GetFormatter(currency), value);
        }
    }
}
