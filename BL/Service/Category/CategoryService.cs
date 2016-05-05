using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BL.Helpers.Data;

namespace BL.Service
{
    public class CategoryService : BaseModifiableCrudService<Category, DAL.Data.Category, Categories, CategoryFilter, CategoryModifier>, ICategoryService
    {
        public async Task<IList<Tuple<Category, decimal, decimal>>> GroupTransactions(IList<Transaction> transactions)
        {
            var output = new List<Tuple<Category, decimal, decimal>>();
            var innerTransactions = transactions.Select(x => new TransactionData{ Description = x.Description, Amount = x.Amount });

            var modifier = new CategoryModifier() { IncludeRules = true };
            var categories = await GetAllAsync(modifier);
            foreach(var category in categories)
            {
                decimal expenses = 0;
                decimal incomes = 0;
                bool add = false;
                foreach (var rule in category.Rules)
                {
                    foreach (var transaction in innerTransactions)
                    {
                        if (rule.Fits(transaction.Description))
                        {
                            if (transaction.Amount < 0)
                                expenses += -transaction.Amount;
                            else
                                incomes += transaction.Amount;

                            if (!transaction.Processed)
                                transaction.Processed = true;
                            if (!add)
                                add = true;
                        }
                    }
                }

                if (add)
                    output.Add(Tuple.Create(category, incomes, expenses));
            }

            var uncategorized = innerTransactions.Where(x => !x.Processed).Select(x => x.Amount);
            if (uncategorized.Any())
                output.Add(Tuple.Create((Category)null, uncategorized.Where(x => x > 0).Sum(), uncategorized.Where(x => x < 0).Sum() * -1));

            return output;
        }
    }
}
