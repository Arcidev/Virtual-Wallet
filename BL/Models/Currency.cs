using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Currency : IDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public float ExchangeRate { get; set; }

        public bool IsDefaultCurrency { get; set; }

        public override string ToString()
        {
            return Code;
        }
    }
}
