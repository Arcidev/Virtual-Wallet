using BL.Models.TransactionModels;
using FioSdkCsharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Filter = BL.Filters.TransactionFilter;
using FioFilter = FioSdkCsharp.TransactionFilter;

namespace BL.Models.BankModels
{
    public class Fio : Bank
    {
        public string Token { get; set; }

        public override async Task<IList<Transaction>> GetNewTransactions()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            var statement = await explorer.Last();

            return GetTransactions(statement.TransactionList);
        }

        public override async Task<IList<Transaction>> GetTransactions(Filter filter)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            var statement = await explorer.Periods(FioFilter.LastDays(filter.Days));

            return GetTransactions(statement.TransactionList);
        }

        public override async Task SetLastDownloadDate(DateTime date)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            await explorer.SetLastDownloadDate(date);
        }

        private IList<Transaction> GetTransactions(FioSdkCsharp.Models.TransactionList transactionList)
        {
            var transactions = new List<Transaction>();
            foreach (var transaction in transactionList.Transactions)
            {
                transactions.Add(new Transaction()
                {
                    Id = (int)transaction.Id.Value,
                    Amount = transaction.Amount?.Value ?? 0,
                    Source = this,
                    Description = transaction.Comment?.Value,
                    Date = transaction.Date?.Value ?? DateTime.Now,
                    Currency = transaction.Currency?.Value
                });
            }

            return transactions;
        }
    }
}
