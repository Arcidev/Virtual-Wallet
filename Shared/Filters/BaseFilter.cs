using System.Collections.Generic;

namespace Shared.Filters
{
    public abstract class BaseFilter
    {
        public IEnumerable<int> Ids { get; set; }

        public bool OrderDesc { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
