using BL.Models;
using BL.Service;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace VirtualWallet.ViewModels
{
    public class HamburgerPaneViewModel : ViewModelBase
    {
        private readonly IBankService bankService;
        private readonly IWalletService walletService;
        private List<Bank> banks;
        private List<Wallet> wallets;
        private readonly ResourceLoader resources;

        public List<Bank> Banks
        {
            get => banks;
            set
            {
                if (banks == value)
                    return;

                banks = value;
                NotifyPropertyChanged();
            }
        }

        public List<Wallet> Wallets
        {
            get => wallets;
            set
            {
                if (wallets == value)
                    return;

                wallets = value;
                NotifyPropertyChanged();
            }
        }

        public string Text_Banks => resources.GetString("HamburgerPane_Banks");

        public string Text_Wallets => resources.GetString("HamburgerPane_Wallets");

        public string Text_Categories => resources.GetString("HamburgerPane_Categories");

        public string Text_Settings => resources.GetString("HamburgerPane_Settings");

        public string Text_AddCashPaynment => resources.GetString("HamburgerPane_AddCashPaynment");

        public HamburgerPaneViewModel(IBankService bankService, IWalletService walletService, ResourceLoader resources)
        {
            this.bankService = bankService;
            this.walletService = walletService;
            this.resources = resources;
        }

        public async Task LoadDataAsync()
        {
            Banks = await bankService.GetAllAsync(new BankModifier() { IncludeImage = true });
            Wallets = await walletService.GetAllAsync(new WalletModifier() { IncludeImage = true });
        }

        public void ReloadTexts()
        {
            NotifyPropertyChanged(nameof(Text_Banks));
            NotifyPropertyChanged(nameof(Text_Wallets));
            NotifyPropertyChanged(nameof(Text_Categories));
            NotifyPropertyChanged(nameof(Text_Settings));
            NotifyPropertyChanged(nameof(Text_AddCashPaynment));
        }
    }
}
