using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWallet.Models
{
    class Rule
    {
        public int RuleId { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }
        public string Desctiption { get; set; }
        public Icon Icon { get; set; }
        public List<Category> Categories { get; set; }
    }
}
