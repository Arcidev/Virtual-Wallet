
namespace Shared.Filters
{
    public class RuleFilter : BaseFilter
    {
        public string Name { get; set; }

        public string Pattern { get; set; }

        public int? CategoryId { get; set; }
    }
}
