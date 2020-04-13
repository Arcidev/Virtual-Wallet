
namespace BL.Filters
{
    public class TransactionFilter
    {
        public int Days { get; set; }

        public void AddDays(int days) => Days += days;

        public void AddWeeks(int weeks) { Days += weeks * 7; }

        public void AddMonths(int months) { Days += months * 30; }

        public void AddYears(int years) { Days += years * 365; }
    }
}
