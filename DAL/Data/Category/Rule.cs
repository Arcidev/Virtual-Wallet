using SQLite.Net.Attributes;

namespace DAL.Data
{
    public class Rule : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(20)]
        public string Name { get; set; }

        public string Pattern { get; set; }

        public int PatternTypeId { get; set; }

        public int CategoryId { get; set; }

        [Ignore]
        public Category Category { get; set; }
    }
}
