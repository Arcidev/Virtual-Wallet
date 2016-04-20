using System;
using System.Collections.Generic;

namespace BL.Models
{
    public class Wallet : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImageId { get; set; }

        public Image Image { get; set; }

        public Uri ImageUri { get { return Image != null ? new Uri(Image.Path) : null; } }

        public IList<Category> Categories { get; set; }

        public IList<ITransactionSource> TransactionSources { get; set; }

        public static Wallet Create(int walletId)
        {
            var wallet = new Wallet();
            wallet.Id = walletId;
            return wallet;
        }
    }
}
