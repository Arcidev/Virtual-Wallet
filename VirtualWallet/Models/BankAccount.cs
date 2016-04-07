using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Models
{
    class BankAccount : TransactionSource
    {
        public int BankAccId { get; set; }
        public Bank Bank { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
