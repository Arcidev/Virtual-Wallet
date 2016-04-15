using System.Collections.Generic;

namespace BL.Models
{
    public class CashPaynment : ITransactionSource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Transaction> StoredTransactions { get; set; }
    }
}
