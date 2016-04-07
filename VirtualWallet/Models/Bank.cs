using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Models
{
    class Bank
    {
        public int BankId { get; set; }
        public string Name { get; set; }
        public Icon Icon { get; set; }
        public List<BankAccount> Accounts { get; set; }
    }
}
