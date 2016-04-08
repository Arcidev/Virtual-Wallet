using System;

namespace BL.Models.TransactionModels
{
    public class Transaction
    {
        public int Id { get; set; }

        public ITransactionSource Source { get; set; }

        public DateTime? Date { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }
    }
}
