using BL.Models.TransactionModels;
using FioSdkCsharp;
using System.Collections.Generic;
using Filter = BL.Models.TransactionModels.TransactionFilter;
using FioFilter = FioSdkCsharp.TransactionFilter;

namespace BL.Models.BankModels
{
    public class Fio : Bank
    {
        private ApiExplorer explorer;

        public string Token { get; private set; }

        public Fio(string token)
        {
            Token = token;
            explorer = new ApiExplorer(token);
        }

        public override IList<Transaction> ReloadTransactions(Filter filter)
        {
            var statement = explorer.Periods(FioFilter.LastDays(filter.Days)).Result;

            var transactions = new List<Transaction>();
            foreach (var transaction in statement.TransactionList.Transactions)
            {
                transactions.Add(new Transaction()
                {
                    Id = (int)transaction.Id.Value,
                    Amount = transaction.Amount?.Value ?? 0,
                    Source = this,
                    Description = transaction.Comment?.Value,
                    Date = transaction.Date?.Value,
                    Currency = transaction.Currency?.Value
                });
            }

            return transactions;
        }
    }
}
