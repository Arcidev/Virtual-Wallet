using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class PagePayload : IPagePayload
    {
        public IDto Dto { get; set; }
        public Image NewImage { get; set; }
    }
}
