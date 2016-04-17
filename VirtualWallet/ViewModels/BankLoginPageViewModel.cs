using BL.Models;
using BL.Service;

namespace VirtualWallet.ViewModels
{
    public class BankLoginPageViewModel : ViewModelBase
    {
        private IBankService bankService;
        private Bank bank;

        public BankLoginPageViewModel(IBankService bankService)
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
