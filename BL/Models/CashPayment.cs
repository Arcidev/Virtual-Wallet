using System.Collections.Generic;

namespace BL.Models
{
    public class CashPayment : ITransactionSource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Transaction> StoredTransactions { get; set; }
    }
}
