using BL.Filters;
using BL.Models;
using BL.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using VirtualWallet.Controls;
using Windows.UI.Core;

namespace VirtualWallet.ViewModels
{
    public class BankPageViewModel : ViewModelBase, IDisposable
    {
        private IBankService bankService;
        private Bank bank;
        private ObservableCollection<Transaction> transactions;
        private BankAccountInfo bankAccountInfo;
        private ICommand syncCommand;
        private Timer syncExecuteTimer;

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
            set
            {
                if (bank == value)
                    return;

                bank = value;
                NotifyPropertyChanged();

                BankAccountInfo = bank?.BankAccountInfo ?? new BankAccountInfo();
                SyncCommand = new CommandHandler(SyncExecute, () => Bank?.NextPossibleSyncTime <= DateTime.Now);
                SetSyncExecuteTimer();
            }
        }

        public ObservableCollection<Transaction> Transactions
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

        public BankPageViewModel(IBankService bankService)
        {
            this.bankService = bankService;
        }

        public void Dispose()
        {
            syncExecuteTimer.Dispose();
        }

        private async void SyncExecute()
        {
            var filter = new TransactionFilter() { Days = 30 };
            var Transactions = await Bank.GetTransactionsAsync(filter);
            BankAccountInfo = Bank.BankAccountInfo;
            NotifyPropertyChanged(nameof(SyncCommand));
            SetSyncExecuteTimer();
        }

        private void SetSyncExecuteTimer()
        {
            if (syncExecuteTimer != null)
                syncExecuteTimer.Dispose();

            var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            if (!SyncCommand.CanExecute(null) && Bank != null)
                syncExecuteTimer = new Timer(async (obj) => await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => NotifyPropertyChanged(nameof(SyncCommand))), null, (int)Math.Ceiling((Bank.NextPossibleSyncTime - DateTime.Now).TotalMilliseconds), Timeout.Infinite);
        }
    }
}
