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

        public string ClosingBalanceString { get { return $"{ClosingBalance} {Currency}"; } }

        public string OpeningBalanceString { get { return $"{OpeningBalance} {Currency}"; } }

        public string DateStartString { get { return ParseDate(DateStart); } }

        public string DateEndString { get { return ParseDate(DateEnd); } }

        private string ParseDate(string date)
        {
            if (date == null)
                return null;

            DateTime result;
            var success = DateTime.TryParse(date, out result);
            return success ? result.ToString("dd.MM.yyyy") : date;
        }
    }
}
