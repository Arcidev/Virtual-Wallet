using BL.Models;
using System;

namespace VirtualWallet.ViewModels
{
    public class BankCredentialsPageViewModel : ViewModelBase
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
            set
            {
                if (bank == value)
                    return;

                bank = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsValid { get { return !string.IsNullOrWhiteSpace(Token); } }

        public BankCredentialsPageViewModel()
        {
            rememberCredentials = true;
        }

        public void SetCredentials()
        {
            if (Bank == null)
                throw new InvalidOperationException("Property Bank must be set");

            Bank.SetCredentials(Token);
            if (RememberCredentials)
                Bank.SaveCredentials();
        }
    }
}
