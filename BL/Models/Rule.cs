using Shared.Enums;
using System.Collections.Generic;

namespace BL.Models
{
    public class Rule : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Pattern { get; set; }

        public PatternType PatternType { get; set; }

        public string Description { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
