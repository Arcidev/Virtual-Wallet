using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class Wallets : BaseModifiableGetDataAccess<Wallet, WalletFilter, WalletModifier>, IWallets
    {
        private static readonly IImages images = new Images();

        protected override AsyncTableQuery<Wallet> ApplyFilters(AsyncTableQuery<Wallet> query, WalletFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.ImageId.HasValue)
                query = query.Where(x => x.ImageId == filter.ImageId.Value);

            return query;
        }

        protected override async Task ApplyModifiersAsync(Wallet wallet, WalletModifier modifier)
        {
            if ((modifier.IncludeImage || modifier.IncludeAll) && wallet.ImageId.HasValue)
                wallet.Image = await images.GetAsync(wallet.ImageId.Value);
        }
    }
}
