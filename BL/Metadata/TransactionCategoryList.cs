using BL.Models;
using Shared.Formatters;
using System.Collections.Generic;
using System.Linq;

namespace BL.Metadata
{
    public class TransactionCategoryList
    {
        private IList<TransactionMetadata> transactions;

        public Category Category { get; set; }

        public IList<TransactionMetadata> Transactions
        {
            get { return transactions; }
            set
            {
                if (value == transactions)
                    return;

                if (value == null)
                {
                    transactions = null;
                    return;
                }

                transactions = new List<TransactionMetadata>();
                var index = 0;
                foreach (var item in value.OrderByDescending(x => x.Date))
                {
                    item.Index = index++;
                    transactions.Add(item);
                }
            }
        }

        public string DefaultCategoryName { get; set; }

        public string CategoryName { get { return Category == null ? DefaultCategoryName : Category.Name; } }

        public string TotalAmount
        {
            get
            {
                return Transactions.Any() ? CurrencyFormatter.Format(Transactions.Sum(x => x.Amount), Transactions.First().Currency) : null;
            }
        }
    }
}
