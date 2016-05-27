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

        public string ClosingBalanceString { get { return CurrencyFormatter.Format(ClosingBalance, Currency); } }

        public string OpeningBalanceString { get { return CurrencyFormatter.Format(OpeningBalance, Currency); } }

        public string DateStartString { get { return ParseDate(DateStart); } }

        public string DateEndString { get { return ParseDate(DateEnd); } }

        public DateTime DateStartAsDate { get { return DateTime.Parse(DateStart); } }

        public DateTime DateEndAsDate { get { return DateTime.Parse(DateEnd); } }

        private string ParseDate(string date)
        {
            if (date == null)
                return null;

            DateTime result;
            var success = DateTime.TryParse(date, out result);
            return success ? DateTimeFormatter.ToShortDate(result) : date;
        }
    }
}
