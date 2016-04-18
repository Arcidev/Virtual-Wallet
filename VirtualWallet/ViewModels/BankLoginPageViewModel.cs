using BL.Models;

namespace VirtualWallet.ViewModels
{
    public class BankLoginPageViewModel : ViewModelBase
    {
        private Bank bank;

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
