using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modifiers
{
    public class WalletCategoryModifier : BaseModifier
    {
        public bool IncludeWallet { get; set; }
        public bool IncludeCategory { get; set; }
    }
}
