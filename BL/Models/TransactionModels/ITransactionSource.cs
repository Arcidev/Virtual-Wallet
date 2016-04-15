using System.Collections.Generic;

namespace BL.Models
{
    public interface ITransactionSource : IDto
    {
        string Name { get; set; }

        IList<Transaction> StoredTransactions { get; set; }
    }
}
