using BL.Filters;
using BL.Models.TransactionModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Models.BankModels
{
    public abstract class Bank : ITransactionSource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Icon Icon { get; set; }

        public IList<Transaction> StoredTransactions { get; set; }

        public abstract Task SetLastDownloadDate(DateTime date);

        public abstract Task<IList<Transaction>> GetNewTransactions();

        public abstract Task<IList<Transaction>> GetTransactions(TransactionFilter filter);
    }
}
