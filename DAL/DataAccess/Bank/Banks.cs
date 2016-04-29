using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace DAL.DataAccess
{
    public class Banks : BaseModifiableGetDataAccess<Bank, BankFilter, BankModifier>, IBanks
    {
        private static readonly IImages images = new Images();
        private static readonly IBankAccountInfoes bankAccountInfoes = new BankAccountInfoes();
        private static readonly ITransactions transactions = new Transactions();

        protected override AsyncTableQuery<Bank> ApplyFilters(AsyncTableQuery<Bank> query, BankFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.ImageId.HasValue)
                query = query.Where(x => x.ImageId == filter.ImageId.Value);

            return query;
        }

        protected override async Task ApplyModifiersAsync(Bank bank, BankModifier modifier)
        {
            if ((modifier.IncludeImage || modifier.IncludeAll) && bank.ImageId.HasValue)
                bank.Image = await images.GetAsync(bank.ImageId.Value);

            if (modifier.IncludeBankAccountInfo || modifier.IncludeAll)
                bank.BankAccountInfo = await bankAccountInfoes.GetAsync(bank.Id);

            if (modifier.IncludeTransactions || modifier.IncludeAll)
                bank.StoredTransactions = await transactions.GetByBankIdAsync(bank.Id);
        }
    }
}
