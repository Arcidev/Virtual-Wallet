using System;

namespace Shared.Filters
{
    public class TransactionFilter : BaseFilter
    {
        public DateTime? DateSince { get; set; }
    }
}
