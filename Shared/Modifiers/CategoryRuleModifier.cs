using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modifiers
{
    public class CategoryRuleModifier : BaseModifier
    {
        public bool IncludeRule { get; set; }
        public bool IncludeCategory { get; set; }
    }
}
