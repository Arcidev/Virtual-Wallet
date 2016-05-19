using BL.Models;
using BL.Service;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWallet.Controls;
using Windows.ApplicationModel.Resources;

namespace VirtualWallet.ViewModels
{
    public class HamburgerPaneViewModel : ViewModelBase
    {
        private IBankService bankService;
        private ICategoryService categoryService;
        private IWalletService walletService;
        private IList<Bank> banks;
        private IList<Wallet> wallets;
        private ResourceLoader resources;

        public IList<Bank> Banks
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

        public IList<Wallet> Wallets
        {
            get { return wallets; }
            set
            {
                if (wallets == value)
                    return;

                wallets = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ReloadTextsCommand { get; private set; }

        public string Text_Banks { get { return resources.GetString("HamburgerPane_Banks"); } }

        public string Text_Wallets { get { return resources.GetString("HamburgerPane_Wallets"); } }

        public string Text_Categories { get { return resources.GetString("HamburgerPane_Categories"); } }

        public string Text_Settings { get { return resources.GetString("HamburgerPane_Settings"); } }

        public HamburgerPaneViewModel(IBankService bankService, IWalletService walletService, ICategoryService categoryService, ResourceLoader resources)
        {
            this.bankService = bankService;
            this.walletService = walletService;
            this.categoryService = categoryService;
            this.resources = resources;
            ReloadTextsCommand = new CommandHandler(ReloadTextsExecute);
        }

        public async Task LoadDataAsync()
        {
            Banks = await bankService.GetAllAsync(new BankModifier() { IncludeImage = true });
            Wallets = await walletService.GetAllAsync(new WalletModifier() { IncludeImage = true });
        }

        private void ReloadTextsExecute()
        {
            NotifyPropertyChanged(nameof(Text_Banks));
            NotifyPropertyChanged(nameof(Text_Wallets));
            NotifyPropertyChanged(nameof(Text_Categories));
            NotifyPropertyChanged(nameof(Text_Settings));
        }
    }
}
