using BL.Models;
using BL.Service;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    public class HamburgerPaneViewModel : ViewModelBase
    {
        private IBankService bankService;
        private IList<Bank> banks { get; set; }

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

        public HamburgerPaneViewModel(IBankService bankService)
        {
            this.bankService = bankService;
        }

        public async Task LoadDataAsync()
        {
            var bankModifier = new BankModifier() { IncludeImage = true };
            Banks = await bankService.GetAllAsync(bankModifier);
        }
    }
}
