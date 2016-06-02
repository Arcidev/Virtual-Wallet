using BL.Metadata;
using BL.Models;
using BL.Service;
using Shared.Enums;
using Shared.Filters;
using Shared.Formatters;
using Shared.Modifiers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWallet.Controls;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

namespace VirtualWallet.ViewModels
{
    public class CashPaymentViewModel : ViewModelBase
    {
        private ITransactionService transactionService;
        private ICurrencyService currencyService;
        private ResourceLoader resources;

        decimal paymentAmount;
        string paymentDescription;
        Currency paymentCurrency;
        TimeRange timeRange;
        private IList<Currency> currencies;
        private IList<Transaction> transactions;

        public CashPaymentViewModel(ITransactionService transactionService, ICurrencyService currencyService, ResourceLoader resources)
        {
            this.transactionService = transactionService;
            this.currencyService = currencyService;
            this.resources = resources;

            this.timeRange = TimeRange.Month;
        }

        public IList<Currency> Currencies
        {
            get { return currencies; }
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
            get { return transactions; }
            set
            {
                if (transactions == value)
                    return;

                transactions = value;
                NotifyPropertyChanged();
            }
        }

        public decimal PaymentAmount
        {
            get
            {
                return paymentAmount;
            }
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
            get
            {
                return paymentDescription;
            }
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
            get
            {
                return paymentCurrency;
            }
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
            get
            {
                return timeRange;
            }
            set
            {
                if (timeRange == value)
                    return;

                timeRange = value;
                NotifyPropertyChanged();
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
            var trans = await transactionService.GetByBankIdAsync(null, filter); //GetAsync(filter);
            trans.OrderByDescending(x => x.Date);
            Transactions = trans;
        }

        public async Task CreateTransaction()
        {
            var newTransaction = new Transaction() { BankId = null, Date = DateTime.Now, Description = paymentDescription, Currency = paymentCurrency.Code, Amount = paymentAmount };
            await transactionService.InsertAsync(false, newTransaction);
            await LoadTransactions();
        }
    }
}
