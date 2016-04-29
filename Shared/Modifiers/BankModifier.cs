
namespace Shared.Modifiers
{
    public class BankModifier : BaseModifier
    {
        public bool IncludeImage { get; set; }

        public bool IncludeBankAccountInfo { get; set; }

        public bool IncludeTransactions { get; set; }
    }
}
