using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class CategoryRule : IDto
    {
        public int Id { get; set; }

        public int RuleId { get; set; }
        
        public Rule Rule { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
