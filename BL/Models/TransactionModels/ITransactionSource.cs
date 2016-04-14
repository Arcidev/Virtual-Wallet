using System.Collections.Generic;

namespace BL.Models.TransactionModels
{
    public interface ITransactionSource
    {
        int Id { get; set; }

        string Name { get; set; }

        IList<Transaction> StoredTransactions { get; set; }
    }
}
