using DAL.Data;
using DAL.Helpers;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class Banks : BaseDataAccess<Bank>, IBanks
    {
        private static readonly IIcons icons = new Icons();

        public async Task<IList<Bank>> GetAll(BankModifier modifier)
        {
            var banks = await GetAll();
            if (modifier != null)
                await ApplyModifiers(banks, modifier);

            return banks;
        }

        public async Task<IList<Bank>> Get(BankFilter filter = null)
        {
            return await Get(filter, null);
        }

        public async Task<IList<Bank>> Get(BankFilter filter, BankModifier modifier)
        {
            if (filter == null)
                filter = new BankFilter();

            var connection = ConnectionHelper.GetDbAsyncConnection();
            var query = connection.Table<Bank>();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.IconId.HasValue)
                query = query.Where(x => x.IconId == filter.IconId.Value);

            var banks = await ApplyBaseFilters(query, filter).ToListAsync();
            if (modifier != null)
                await ApplyModifiers(banks, modifier);

            return banks;
        }

        public async Task<Bank> Get(int id, BankModifier modifier)
        {
            var bank = await Get(id);
            if (modifier != null && bank != null)
                await ApplyModifiers(bank, modifier);

            return bank;
        }

        private async Task ApplyModifiers(Bank banks, BankModifier modifier)
        {
            if (modifier.IncludeIcon && banks.IconId.HasValue)
                banks.Icon = await icons.Get(banks.IconId.Value);
        }

        private async Task ApplyModifiers(IList<Bank> banks, BankModifier modifier)
        {
            foreach (var bank in banks)
                await ApplyModifiers(bank, modifier);
        }
    }
}
