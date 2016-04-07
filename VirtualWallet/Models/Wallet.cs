using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Models
{
    class Wallet
    {
        public int WalletId { get; set; }
        public string Name { get; set; }
        public Icon Icon { get; set; }
        public List<Category> Categories { get; set; }
        public List<TransactionSource> TransactionSources { get; set; }
    }
}
