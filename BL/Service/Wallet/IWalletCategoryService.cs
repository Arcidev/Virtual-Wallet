using BL.Models;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public interface IWalletCategoryService : IModifiableCrudService<WalletCategory, WalletCategoryFilter, WalletCategoryModifier>
    {
    }
}
