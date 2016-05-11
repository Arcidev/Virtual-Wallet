using BL.Models;
using BL.Service;
using Shared.Filters;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class BanksPageViewModel : ViewModelBase
    {
        private IBankService bankService;
        private IWalletBankService walletBankService;

        private Wallet wallet;
        private ObservableCollection<Bank> banks;

        private Bank selectedBank;

        public BanksPageViewModel(IBankService bankService, IWalletBankService walletBankService)
        {
            this.bankService = bankService;
            this.walletBankService = walletBankService;
        }

        public Wallet Wallet
        {
            get { return wallet; }
            set
            {
                if (wallet == value)
                    return;

                wallet = value;
                NotifyPropertyChanged();
            }
        }

        public Bank SelectedBank
        {
            get { return selectedBank; }
            set
            {
                if (selectedBank == value)
                    return;

                selectedBank = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Bank> Banks
        {
            get { return banks; }
            set
            {
                if (banks == value)
                    return;

                banks = value;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadDataAsync()
        {
            var bankModifier = new BankModifier() { IncludeImage = true };
            var banks = await bankService.GetAllAsync(bankModifier);
            
            if (Wallet != null)
            {
                var walCatModifier = new WalletBankModifier() { IncludeBank = true };
                var walCatfilter = new WalletBankFilter() { WalletId = Wallet.Id };
                var walletsBanks = await walletBankService.GetAsync(walCatfilter, walCatModifier);
                
                foreach (var walletBank in walletsBanks)
                {
                    banks.Remove(walletBank.Bank);
                }
            }
            
            Banks = new ObservableCollection<Bank>(banks);
        }

        public async Task SaveRelationAsync()
        {
            if (Wallet != null && SelectedBank != null)
            {
                var walCat = new WalletBank() { WalletId = Wallet.Id, BankId = SelectedBank.Id};
                await walletBankService.InsertAsync(false, walCat);
            }
        }
    }
}
