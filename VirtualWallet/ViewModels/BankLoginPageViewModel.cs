using BL.Models;

namespace VirtualWallet.ViewModels
{
    public class BankLoginPageViewModel : ViewModelBase
    {
        private Bank bank;
        private string token;
        private bool rememberCredentials;

        public string Token
        {
            get { return token; }
            set
            {
                if (token == value)
                    return;

                token = value;
                NotifyPropertyChanged();
            }
        }

        public bool RememberCredentials
        {
            get { return rememberCredentials; }
            set
            {
                if (rememberCredentials == value)
                    return;

                rememberCredentials = value;
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
            }
        }

        public bool IsValid { get { return !string.IsNullOrWhiteSpace(Token); } }

        public BankLoginPageViewModel()
        {
            rememberCredentials = true;
        }

        public void LoadData(Bank bank)
        {
            Bank = bank;
        }

        public void SetCredentials()
        {
            Bank.SetCredentials(Token);
            if (RememberCredentials)
                Bank.SaveCredentials();
        }
    }
}
