using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class WalletCategory : IDao
    {
        [PrimaryKey]
        public int Id { get; set; }
        
        public int WalletId { get; set; }

        [NotNull, Ignore]
        public Wallet Wallet { get; set; }
        
        public int CategoryId { get; set; }

        [NotNull, Ignore]
        public Category Category { get; set; }
    }
}
