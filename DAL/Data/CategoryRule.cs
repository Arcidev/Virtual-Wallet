using SQLite.Net.Attributes;

namespace DAL.Data
{
    public class CategoryRule : IDao
    {
        [PrimaryKey]
        public int Id { get; set; }
        
        public int RuleId { get; set; }

        [Ignore]
        public Rule Rule { get; set; }
        
        public int CategoryId { get; set; }

        [Ignore]
        public Category Category { get; set; }
    }
}
