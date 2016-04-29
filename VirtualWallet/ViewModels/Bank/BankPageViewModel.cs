using BL.Filters;
using BL.Models;
using BL.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWallet.Controls;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace VirtualWallet.ViewModels
{
    public class BankPageViewModel : ViewModelBase, IDisposable
    {
        private IBankAccountInfoService bankAccountInfoService;
        private ITransactionService transactionService;
        private ResourceLoader resources;
        private Bank bank;
        private IList<Transaction> transactions;
        private BankAccountInfo bankAccountInfo;
        private ICommand syncCommand;
        private Timer syncExecuteTimer;
        private bool syncButtonForceDisabled;

        public ICommand SyncCommand
        {
            get { return syncCommand; }
            set
            {
                if (syncCommand == value)
                    return;

                syncCommand = value;
                NotifyPropertyChanged();
            }
        }

        public Bank Bank
        {
            get { return bank; }
            private set
            {
                if (bank == value)
                    return;

                bank = value;
                NotifyPropertyChanged();

                BankAccountInfo = Bank?.BankAccountInfo ?? new BankAccountInfo();
                Transactions = Bank?.StoredTransactions;
                SyncCommand = new CommandHandler(SyncExecute, () => !syncButtonForceDisabled && (Bank?.CanSyncExecute ?? false));
                SetSyncExecuteTimer();
            }
        }

        public IList<Transaction> Transactions
        {
            get { return transactions; }
            set
            {
                if (transactions == value)
                    return;

                transactions = value;
                NotifyPropertyChanged();
            }
        }

        public BankAccountInfo BankAccountInfo
        {
            get { return bankAccountInfo; }
            set
            {
                if (bankAccountInfo == value)
                    return;

                bankAccountInfo = value;
                NotifyPropertyChanged();
            }
        }

        public BankPageViewModel(IBankAccountInfoService bankAccountInfoService, ITransactionService transactionService, ResourceLoader resources)
        {
            this.bankAccountInfoService = bankAccountInfoService;
            this.transactionService = transactionService;
            this.resources = resources;
        }

        public async Task LoadDataAsync(Bank bank)
        {
            if (bank != null)
            {
                bank.BankAccountInfo = await bankAccountInfoService.GetAsync(bank.Id);
                bank.StoredTransactions = await transactionService.GetByBankIdAsync(bank.Id);
            }

            Bank = bank;
        }

        public void Dispose()
        {
            syncExecuteTimer?.Dispose();
        }

        private async void SyncExecute()
        {
            // Disable button immediatly after invoking command
            syncButtonForceDisabled = true;
            NotifyPropertyChanged(nameof(SyncCommand));

            var filter = new TransactionFilter();
            filter.AddMonths(1);

            try
            {
                var bankTransactions = await Bank.GetTransactionsAsync(filter);
                BankAccountInfo = Bank.BankAccountInfo;

                if (Bank.StoredTransactions != null && Bank.StoredTransactions.Any())
                {
                    var toStore = new List<Transaction>();
                    foreach (var transaction in bankTransactions)
                    {
                        if (!Bank.StoredTransactions.Any(x => x.ExternalId == transaction.ExternalId))
                        {
                            Bank.StoredTransactions.Add(transaction);
                            toStore.Add(transaction);
                        }
                    }

                    await transactionService.InsertOrIgnoreAsync(toStore.ToArray());
                }
                else
                {
                    Bank.StoredTransactions = bankTransactions;
                    await transactionService.InsertOrIgnoreAsync(bankTransactions.ToArray());
                }

                transactions = Bank.StoredTransactions;
                NotifyPropertyChanged(nameof(Transactions));
                await bankAccountInfoService.InsertOrReplaceAsync(BankAccountInfo);
            }
            catch (Exception)
            {
                var dialog = new MessageDialog(resources.GetString("Bank_ErrorDialog"));
                dialog.Commands.Add(new UICommand(resources.GetString("Dialog_Close")));

                await dialog.ShowAsync();
            }

            syncButtonForceDisabled = false;
            NotifyPropertyChanged(nameof(SyncCommand));
            SetSyncExecuteTimer();
        }

        private void SetSyncExecuteTimer()
        {
            if (syncExecuteTimer != null)
                syncExecuteTimer.Dispose();

            if (!(Bank?.CanSyncExecute ?? true))
            {
                var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                syncExecuteTimer = new Timer(async (obj) => await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => NotifyPropertyChanged(nameof(SyncCommand))), null, (int)Math.Ceiling((Bank.NextPossibleSyncTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
            }
        }
    }
}
