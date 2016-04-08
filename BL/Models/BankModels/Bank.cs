using BL.Models.TransactionModels;
using System.Collections.Generic;

namespace BL.Models.BankModels
{
    public abstract class Bank : ITransactionSource
    {
        private IList<Transaction> transactions;

        public int Id { get; set; }

        public string Name { get; set; }

        public Icon Icon { get; set; }

        public IList<Transaction> Transactions
        {
            get
            {
                if (transactions != null)
                    return transactions;

                transactions = ReloadTransactions(new TransactionFilter() { Days = 30 });
                return transactions;
            }
            set { transactions = value; }
        }

        public abstract IList<Transaction> ReloadTransactions(TransactionFilter filter);
    }
}
