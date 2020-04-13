using Shared.Formatters;
using System;

namespace BL.Metadata
{
    public class TransactionMetadata : IIndexable
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }

        internal bool Processed { get; set; }

        public DateTime Date { get; set; }

        public string Currency { get; set; }

        public string DateString => DateTimeFormatter.ToShortDate(Date);

        public string AmountString => CurrencyFormatter.Format(Amount, Currency);

        public int Index { get; set; }
    }
}
