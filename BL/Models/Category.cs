using System.Collections.Generic;

namespace BL.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Icon Icon { get; set; }

        public IList<Rule> Rules { get; set; }

        public IList<Wallet> Wallets { get; set; }
    }
}
