using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Service;

namespace VirtualWallet.ViewModels
{
    class MenuPageViewModel : ViewModelBase
    {
        private BankService bankService;
        private CategoryService categoryService;
        private WalletService walletService;

        public MenuPageViewModel(WalletService walletService, BankService bankService, CategoryService categoryService)
        {
            this.walletService = walletService;
            this.bankService = bankService;
            this.categoryService = categoryService;
        }

        public async Task LoadData()
        {
            
        }
    }
}
