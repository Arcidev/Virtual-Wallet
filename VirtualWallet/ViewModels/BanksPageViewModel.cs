using BL.Models;
using BL.Service;
using Shared.Modifiers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VirtualWallet.ViewModels
{
    public class BanksPageViewModel : ViewModelBase
    {
        private IBankService bankService;
        private ObservableCollection<Bank> banks;

        public BanksPageViewModel(IBankService bankService)
        {
            this.bankService = bankService;
        }

        public ObservableCollection<Bank> Banks
        {
            get { return banks; }
            set
            {
                banks = value;
                NotifyPropertyChanged();
            }
        }

        public async Task LoadData()
        {
            var modifier = new BankModifier() { IncludeImage = true };
            Banks = new ObservableCollection<Bank>(await bankService.GetAll(modifier));
        }
    }
}
