using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Models
{
    class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Icon Icon { get; set; }
        public List<Rule> Rules { get; set; }
        public List<Wallet> Wallets { get; set; }
    }
}
