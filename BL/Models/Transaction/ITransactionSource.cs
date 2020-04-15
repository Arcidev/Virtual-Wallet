using System.Collections.Generic;

namespace BL.Models
{
    public interface ITransactionSource : IDto
    {
        string Name { get; set; }

        List<Transaction> StoredTransactions { get; set; }
    }
}
