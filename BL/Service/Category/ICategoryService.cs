using BL.Models;
using Shared.Filters;
using Shared.Modifiers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public interface ICategoryService : IModifiableCrudService<Category, CategoryFilter, CategoryModifier>
    {
        Task<IList<Tuple<Category, decimal, decimal>>> GroupTransactions(IList<Transaction> transactions);
    }
}
