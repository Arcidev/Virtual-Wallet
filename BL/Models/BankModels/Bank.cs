using BL.Filters;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace BL.Models
{
    public abstract class Bank : ITransactionSource, IDto
    {
        private static readonly Dictionary<BankId, Type> derrivedClasses = new Dictionary<BankId, Type>()
        {
            { BankId.Fio, typeof(Fio) }
        };

        protected PasswordVault PasswordVault { get { return new PasswordVault(); } }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImageId { get; set; }

        public Image Image { get; set; }

        public Uri ImageUri { get { return Image != null ? new Uri(Image.Path) : null; } }

        public BankAccountInfo BankAccountInfo { get; set; }

        public IList<Transaction> StoredTransactions { get; set; }

        public bool CanSyncExecute { get { return NextPossibleSyncTime <= DateTime.Now; } }

        public abstract CredentialType CredentialType { get; }

        public abstract bool HasCredentials { get; }

        public abstract DateTime NextPossibleSyncTime { get; }

        public abstract void SetCredentials(string token);

        public abstract void SaveCredentials();

        public abstract void RemoveCredentials();

        public abstract Task SetLastDownloadDateAsync(DateTime date);

        public abstract Task<IList<Transaction>> GetNewTransactionsAsync();

        public abstract Task<IList<Transaction>> GetTransactionsAsync(TransactionFilter filter);

        public static Bank Create(int bankId)
        {
            return Activator.CreateInstance(derrivedClasses[(BankId)bankId]) as Bank;
        }
    }
}
