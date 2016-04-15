using System.Collections.Generic;

namespace BL.Models.TransactionModels
{
    public interface ITransactionSource : IDto
    {
        string Name { get; set; }

        IList<Transaction> StoredTransactions { get; set; }
    }
}
