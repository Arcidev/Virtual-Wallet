using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Models
{
    class TransactionSource
    {
        public int TransSrcId { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
