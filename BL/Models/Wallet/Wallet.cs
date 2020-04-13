using Shared.Enums;
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

        public Uri ImageUri => Image != null ? new Uri(Image.Path) : null;

        public IList<Category> Categories { get; set; }

        public IList<ITransactionSource> TransactionSources { get; set; }

        public PagePayload PagePayload => new PagePayload() { Dto = this };

        public TimeRange TimeRange { get; set; }

        public int? CurrencyId { get; set; }
    }
}
