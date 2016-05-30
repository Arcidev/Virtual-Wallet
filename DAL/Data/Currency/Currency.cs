using SQLite.Net.Attributes;

namespace DAL.Data
{
    public class Currency : IDao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(3)]
        public string Code { get; set; }

        public float ExchangeRate { get; set; }

        public bool IsDefaultCurrency { get; set; }
    }
}
