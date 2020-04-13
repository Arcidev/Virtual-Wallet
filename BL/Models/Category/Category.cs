using System;
using System.Collections.Generic;

namespace BL.Models
{
    public class Category : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImageId { get; set; }

        public Image Image { get; set; }

        public Uri ImageUri => Image != null ? new Uri(Image.Path) : null;

        public IList<Rule> Rules { get; set; }

        public IList<Wallet> Wallets { get; set; }

        public PagePayload PagePayload => new PagePayload() { Dto = this };
    }
}
