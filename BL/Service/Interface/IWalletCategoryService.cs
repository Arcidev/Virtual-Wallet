using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IWalletCategoryService : IModifiableGetService<WalletCategory, WalletCategoryFilter, WalletCategoryModifier>
    {
    }
}
