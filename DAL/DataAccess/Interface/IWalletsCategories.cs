using DAL.Data;
using Shared.Filters;
using Shared.Modifiers;

namespace DAL.DataAccess
{
    public interface IWalletsCategories : IModifiableGet<WalletCategory, WalletCategoryFilter, WalletCategoryModifier>
    {
    }
}
