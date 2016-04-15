using System.Collections.Generic;

namespace BL.Models
{
    public class Wallet : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Icon Icon { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<ITransactionSource> TransactionSources { get; set; }
    }
}
