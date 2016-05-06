using Shared.Formatters;
using System;

namespace BL.Models
{
    public class Transaction : IDto
    {
        public int Id { get; set; }

        public long? ExternalId { get; set; }

        public int? BankId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public string DateString { get { return DateTimeFormatter.ToShortDate(Date); } }

        public string AmountString { get { return CurrencyFormatter.Format(Amount, Currency); } }
    }
}
