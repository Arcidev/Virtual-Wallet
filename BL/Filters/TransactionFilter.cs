
namespace BL.Filters
{
    public class TransactionFilter
    {
        private int days;

        public int Days
        {
            get { return days; }
            set { days = value; }
        }

        public void AddDays(int days) { this.days += days; }

        public void AddWeeks(int weeks) { this.days += weeks * 7; }

        public void AddMonths(int months) { this.days += months * 30; }

        public void AddYears(int years) { this.days += years * 365; }
    }
}
