using BL.Models;
using BL.Service;
using Shared.Modifiers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    class WalletsPageViewModel : ViewModelBase
    {
        private IWalletService walletService;
        
        private ObservableCollection<Wallet> wallets;

        public WalletsPageViewModel(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        public ObservableCollection<Wallet> Wallets
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

        public async Task LoadDataAsync()
        {
            var walletModifier = new WalletModifier() { IncludeImage = true };
            var wallets = await walletService.GetAllAsync(walletModifier);
            Wallets = new ObservableCollection<Wallet>(wallets);
        }
    }
}
