using BL.Models;
using BL.Service;
using Shared.Enums;
using Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    public class CashPaymentViewModel : ViewModelBase
    {
        private readonly ITransactionService transactionService;
        private readonly ICurrencyService currencyService;

        DateTime paymentDate;
        string paymentAmount;
        string paymentDescription;
        Currency paymentCurrency;
        TimeRange timeRange;
        private IList<Currency> currencies;
        private IList<Transaction> transactions;

        public CashPaymentViewModel(ITransactionService transactionService, ICurrencyService currencyService)
        {
            this.transactionService = transactionService;
            this.currencyService = currencyService;

            timeRange = TimeRange.Month;
            PaymentDate = DateTime.Now;
        }

        public IList<Currency> Currencies
        {
            get => currencies;
            set
            {
                if (currencies == value)
                    return;

                currencies = value;
                NotifyPropertyChanged();
            }
        }

        public IList<Transaction> Transactions
        {
            get => transactions;
            set
            {
                if (transactions == value)
                    return;

                transactions = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime PaymentDate
        {
            get => paymentDate;
            set
            {
                if (paymentDate == value)
                    return;

                paymentDate = value;
                NotifyPropertyChanged();
            }
        }

        public string PaymentAmount
        {
            get => paymentAmount;
            set
            {
                if (paymentAmount == value)
                    return;

                paymentAmount = value;
                NotifyPropertyChanged();
            }
        }

        public string PaymentDescription
        {
            get => paymentDescription;
            set
            {
                if (paymentDescription == value)
                    return;

                paymentDescription = value;
                NotifyPropertyChanged();
            }
        }

        public Currency PaymentCurrency
        {
            get => paymentCurrency;
            set
            {
                if (paymentCurrency == value)
                    return;

                paymentCurrency = value;
                NotifyPropertyChanged();
            }
        }

        public TimeRange TimeRange
        {
            get => timeRange;
            set
            {
                if (timeRange == value)
                    return;

                timeRange = value;
                NotifyPropertyChanged();
                LoadTransactionsInner();
            }
        }

        public async Task LoadDataAsync()
        {
            await LoadCurrencies();
            await LoadTransactions();
        }

        private async Task LoadCurrencies()
        {
            Currencies = await currencyService.GetAllAsync();
            PaymentCurrency = Currencies.FirstOrDefault(x => x.IsDefaultCurrency);
        }

        private async Task LoadTransactions()
        {
            var filter = new TransactionFilter() { Ids = null, DateSince = TimeRange.ToDateSince() };
            var trans = await transactionService.GetByBankIdAsync(null, filter);
            trans = trans.OrderByDescending(x => x.Date).ToList();
            Transactions = trans;
        }

        public async Task CreateTransaction()
        {
            var amount = decimal.Parse(paymentAmount);
            var newTransaction = new Transaction() { BankId = null, Date = PaymentDate, Description = paymentDescription, Currency = paymentCurrency.Code, Amount = amount };

            await transactionService.InsertAsync(false, newTransaction);
            await LoadTransactions();
        }

        public async Task DeleteTransaction(Transaction t)
        {
            await transactionService.DeleteAsync(t.Id);
            await LoadTransactions();
        }

        private async void LoadTransactionsInner()
        {
            await LoadTransactions();
        }
    }
}
