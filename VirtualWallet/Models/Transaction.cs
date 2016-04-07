using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Models
{
    class Transaction
    {
        public TransactionSource source { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
    }
}
