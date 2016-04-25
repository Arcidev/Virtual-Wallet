using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Filters
{
    public class WalletCategoryFilter : BaseFilter
    {
        public int? WalletId { get; set; }

        public int? CategoryId { get; set; }
    }
}
