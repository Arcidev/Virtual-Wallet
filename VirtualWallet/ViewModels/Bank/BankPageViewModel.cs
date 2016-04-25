using BL.Filters;
using BL.Models;
using BL.Service;
using System;
using System.Collections.ObjectModel;
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
        private ResourceLoader resources;
        private Bank bank;
        private ObservableCollection<Transaction> transactions;
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
                SyncCommand = new CommandHandler(SyncExecute, () => !syncButtonForceDisabled && (Bank?.CanSyncExecute ?? false));
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

        public BankPageViewModel(IBankAccountInfoService bankAccountInfoService, ResourceLoader resources)
        {
            this.bankAccountInfoService = bankAccountInfoService;
            this.resources = resources;
        }

        public async Task LoadDataAsync(Bank bank)
        {
            if (bank != null)
                bank.BankAccountInfo = await bankAccountInfoService.GetAsync(bank.Id);

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

            var filter = new TransactionFilter() { Days = 30 };
            try
            {
                var Transactions = await Bank.GetTransactionsAsync(filter);
                BankAccountInfo = Bank.BankAccountInfo;
                await bankAccountInfoService.ReplaceAsync(BankAccountInfo);
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
