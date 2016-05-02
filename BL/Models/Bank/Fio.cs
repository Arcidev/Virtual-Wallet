using BL.Mapping;
using FioSdkCsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Filter = BL.Filters.TransactionFilter;
using FioFilter = FioSdkCsharp.TransactionFilter;

namespace BL.Models
{
    public class Fio : Bank
    {
        private const string fioResource = "FioBankToken";
        private const string fioUser = "FioUser";
        private const uint syncTimeOutinSec = 30;
        private static DateTime nextPossibleSyncTime;

        public string Token { get; set; }

        public override bool HasCredentials { get { return !string.IsNullOrEmpty(Token); } }

        public override DateTime NextPossibleSyncTime { get { return nextPossibleSyncTime; } }

        public Fio()
        {
            var credentials = GetCredentials();
            if (credentials != null)
            {
                credentials.RetrievePassword();
                Token = credentials.Password;
            }
        }

        public override async Task<IList<Transaction>> GetNewTransactionsAsync()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            var statement = await explorer.LastAsync();
            nextPossibleSyncTime = DateTime.Now.AddSeconds(syncTimeOutinSec);

            return GetTransactions(statement);
        }

        public override async Task<IList<Transaction>> GetTransactionsAsync(Filter filter)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            var statement = await explorer.PeriodsAsync(FioFilter.LastDays(filter.Days));
            nextPossibleSyncTime = DateTime.Now.AddSeconds(syncTimeOutinSec);

            return GetTransactions(statement);
        }

        public override async Task SetLastDownloadDateAsync(DateTime date)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException("Fio bank token has not been set");

            ApiExplorer explorer = new ApiExplorer(Token);
            await explorer.SetLastDownloadDateAsync(date);
        }

        public override void SaveCredentials()
        {
            if (string.IsNullOrEmpty(Token))
                return;

            var password = new PasswordCredential(fioResource, fioUser, Token);
            PasswordVault.Add(password);
        }

        public override void SetCredentials(string token)
        {
            Token = token;
        }

        public override void RemoveCredentials()
        {
            var credentials = GetCredentials();
            if (credentials != null)
                PasswordVault.Remove(credentials);

            Token = null;
        }

        private PasswordCredential GetCredentials()
        {
            return PasswordVault.RetrieveAll().FirstOrDefault(x => x.Resource == fioResource && x.UserName == fioUser);
        }

        private IList<Transaction> GetTransactions(FioSdkCsharp.Models.AccountStatement accountStatement)
        {
            BankAccountInfo = MapperInstance.Mapper.Map<BankAccountInfo>(accountStatement.Info);
            BankAccountInfo.Id = Id;

            var transactions = new List<Transaction>();
            foreach (var transaction in accountStatement.TransactionList.Transactions)
            {
                transactions.Add(new Transaction()
                {
                    ExternalId = transaction.Id?.Value,
                    Amount = transaction.Amount?.Value ?? 0,
                    BankId = Id,
                    Description = transaction.Comment?.Value,
                    Date = transaction.Date?.Value ?? DateTime.Now,
                    Currency = transaction.Currency?.Value
                });
            }

            return transactions;
        }
    }
}
