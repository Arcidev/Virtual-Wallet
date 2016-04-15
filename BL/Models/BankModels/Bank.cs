using BL.Filters;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Models
{
    public abstract class Bank : ITransactionSource, IDto
    {
        private static readonly Dictionary<BankId, Type> derrivedClasses = new Dictionary<BankId, Type>()
        {
            {BankId.Fio, typeof(Fio) }
        };

        public int Id { get; set; }

        public string Name { get; set; }

        public Icon Icon { get; set; }

        public IList<Transaction> StoredTransactions { get; set; }

        public abstract bool HasCredentials { get; }

        public abstract Task SetLastDownloadDateAsync(DateTime date);

        public abstract Task<IList<Transaction>> GetNewTransactionsAsync();

        public abstract Task<IList<Transaction>> GetTransactionsAsync(TransactionFilter filter);

        public static Bank Create(int bankId)
        {
            return Activator.CreateInstance(derrivedClasses[(BankId)bankId]) as Bank;
        }
    }
}
