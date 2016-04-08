using BL.Models.TransactionModels;
using System.Collections.Generic;

namespace BL.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Icon Icon { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<ITransactionSource> TransactionSources { get; set; }
    }
}
