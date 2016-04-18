using System.Collections.Generic;

namespace BL.Models
{
    public class Rule : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Pattern { get; set; }

        public string Desctiption { get; set; }

        public Image Image { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
