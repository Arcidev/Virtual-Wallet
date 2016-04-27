using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace DAL.Data
{
    public class Rule : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Pattern { get; set; }

        [Ignore]
        public IList<Category> categories { get; set; }
    }
}
