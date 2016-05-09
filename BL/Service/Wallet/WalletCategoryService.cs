using BL.Models;
using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;

namespace BL.Service
{
    public class WalletCategoryService : BaseModifiableCrudService<WalletCategory, DAL.Data.WalletCategory, WalletsCategories, WalletCategoryFilter, WalletCategoryModifier>, IWalletCategoryService
    {
    }
}
