﻿using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BL.Metadata;

namespace BL.Service
{
    public class CategoryService : BaseModifiableCrudService<Category, DAL.Data.Category, Categories, CategoryFilter, CategoryModifier>, ICategoryService
    {
        public async Task<IList<TransactionCategoryList>> GroupTransactions(IList<Transaction> transactions, string defaultCategoryName)
        {
            var output = new List<TransactionCategoryList>();
            var innerTransactions = transactions.Select(x => new TransactionMetadata { Description = x.Description, Amount = x.Amount, Date = x.Date, Currency = x.Currency }).ToList();

            var modifier = new CategoryModifier() { IncludeRules = true };
            var categories = await GetAllAsync(modifier);
            foreach(var category in categories)
            {
                var items = new List<TransactionMetadata>();
                bool add = false;
                foreach (var rule in category.Rules)
                {
                    foreach (var transaction in innerTransactions)
                    {
                        if (transaction.Processed)
                            continue;

                        if (rule.Fits(transaction.Description))
                        {
                            items.Add(transaction);
                            transaction.Processed = true;
                            if (!add)
                                add = true;
                        }
                    }
                }

                if (add)
                    output.Add(new TransactionCategoryList { Category = category, Transactions = items });
            }

            var uncategorized = innerTransactions.Where(x => !x.Processed);
            if (uncategorized.Any())
                output.Add(new TransactionCategoryList { DefaultCategoryName = defaultCategoryName, Transactions = uncategorized.ToList() });

            return output;
        }
    }
}
