using BL.Models;
using System;

namespace VirtualWallet.ViewModels
{
    public class BankLoginPageViewModel : ViewModelBase
    {
        private Uri bankImageUri;
        private string token;
        private bool rememberCredentials = true;

        public Uri BankImageUri
        {
            get { return bankImageUri; }
            set
            {
                if (bankImageUri == value)
                    return;

                bankImageUri = value;
                NotifyPropertyChanged();
            }
        }

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

        public void LoadData(Bank bank)
        {
            BankImageUri = bank.ImageUri;
        }
    }
}
