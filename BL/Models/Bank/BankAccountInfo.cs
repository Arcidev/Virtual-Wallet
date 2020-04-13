using Shared.Formatters;
using System;

namespace BL.Models
{
    public class BankAccountInfo : IDto
    {
        public int Id { get; set; }

        public double ClosingBalance { get; set; }

        public string Currency { get; set; }

        public double OpeningBalance { get; set; }

        public string DateStart { get; set; }

        public string DateEnd { get; set; }

        public string ClosingBalanceString => CurrencyFormatter.Format(ClosingBalance, Currency);

        public string OpeningBalanceString => CurrencyFormatter.Format(OpeningBalance, Currency);

        public string DateStartString => ParseDate(DateStart);

        public string DateEndString => ParseDate(DateEnd);

        public DateTime DateStartAsDate => DateTime.Parse(DateStart);

        public DateTime DateEndAsDate => DateTime.Parse(DateEnd);

        private string ParseDate(string date)
        {
            if (date == null)
                return null;

            return DateTime.TryParse(date, out var result) ? DateTimeFormatter.ToShortDate(result) : date;
        }
    }
}
