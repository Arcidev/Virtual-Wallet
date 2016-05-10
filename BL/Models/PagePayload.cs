namespace BL.Models
{
    public class PagePayload : IPagePayload
    {
        public IDto Dto { get; set; }

        public Image NewImage { get; set; }

        public Rule Rule { get; set; }
    }
}
