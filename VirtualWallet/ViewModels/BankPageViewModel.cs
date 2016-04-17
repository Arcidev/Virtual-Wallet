using BL.Models;
using BL.Service;

namespace VirtualWallet.ViewModels
{
    public class BankPageViewModel : ViewModelBase
    {
        private IBankService bankService;
        private Bank bank;

        public BankPageViewModel(IBankService bankService)
        {
            this.bankService = bankService;
        }

        public Bank Bank
        {
            get { return bank; }
            set
            {
                bank = value;
                NotifyPropertyChanged();
            }
        }
    }
}
