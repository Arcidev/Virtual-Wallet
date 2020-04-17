using BL.Metadata;
using BL.Models;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ICategoryService : IModifiableCrudService<Category, CategoryFilter, CategoryModifier>
    {
        Task<IEnumerable<TransactionCategoryList>> GroupTransactions(IEnumerable<Transaction> transactions, string defaultCategoryName);
        IEnumerable<TransactionCategoryList> GroupTransactionsForWallet(IEnumerable<Category> categories, IEnumerable<Transaction> transactions, string defaultCategoryName);
    }
}
