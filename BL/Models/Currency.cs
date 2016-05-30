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
