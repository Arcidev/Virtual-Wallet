using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Filters
{
    public class CategoryRuleFilter : BaseFilter
    {
        public int? RuleId { get; set; }

        public int? CategoryId { get; set; }
    }
}
