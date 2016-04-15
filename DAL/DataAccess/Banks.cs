using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Banks : BaseModifiableGetDataAccess<Bank, BankFilter, BankModifier>, IBanks
    {
        private static readonly IIcons icons = new Icons();

        protected override AsyncTableQuery<Bank> ApplyFilters(AsyncTableQuery<Bank> query, BankFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.IconId.HasValue)
                query = query.Where(x => x.IconId == filter.IconId.Value);

            return query;
        }

        protected override async Task ApplyModifiers(Bank bank, BankModifier modifier)
        {
            if ((modifier.IncludeIcon || modifier.IncludeAll) && bank.IconId.HasValue)
                bank.Icon = await icons.Get(bank.IconId.Value);
        }
    }
}
