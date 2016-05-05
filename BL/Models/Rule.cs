using Shared.Enums;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public bool Fits(string val)
        {
            // Always fits to no pattern
            if (Pattern == null)
                return true;

            // Never fits to pattern
            if (val == null)
                return false;

            switch (PatternType)
            {
                case PatternType.StartsWith:
                    return val.StartsWith(Pattern);
                case PatternType.EndsWith:
                    return val.EndsWith(Pattern);
                case PatternType.Contains:
                    return val.Contains(Pattern);
                case PatternType.Regex:
                    return Regex.IsMatch(val, Pattern);
                default:
                    return false;
            }
        }
    }
}
