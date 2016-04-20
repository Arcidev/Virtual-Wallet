using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class Wallet : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        public int? ImageId { get; set; }

        [Ignore]
        public Image Image { get; set; }
    }
}