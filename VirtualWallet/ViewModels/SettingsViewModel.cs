using BL.Service;
using System.Windows.Input;
using VirtualWallet.Controls;

namespace VirtualWallet.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private IDatabaseService databaseService;
        private IBankService bankService;

        public ICommand RemoveAllDataCommand { get; set; }

        public ICommand RemoveAllCredentialsCommand { get; set; }

        public ICommand CopyDatabaseToRoamingFolderCommand { get; set; }

        public ICommand RetrieveDatabaseFromRoamingFolderCommand { get; set; }

        public SettingsViewModel(IDatabaseService databaseService, IBankService bankService)
        {
            this.databaseService = databaseService;
            this.bankService = bankService;
            RemoveAllDataCommand = new CommandHandler(RemoveAllDataExecute);
            RemoveAllCredentialsCommand = new CommandHandler(RemoveAllCredentialsExecute);
            CopyDatabaseToRoamingFolderCommand = new CommandHandler(CopyDatabaseToRoamingFolderExecute);
            RetrieveDatabaseFromRoamingFolderCommand = new CommandHandler(RetrieveDatabaseFromRoamingFolderExecute);
        }

        private async void RemoveAllDataExecute()
        {
            await databaseService.RemoveAllDataAsync();
            RemoveAllCredentialsExecute();
        }

        private async void RemoveAllCredentialsExecute()
        {
            foreach (var bank in await bankService.GetAll())
                bank.RemoveCredentials();
        }

        private async void CopyDatabaseToRoamingFolderExecute()
        {
            await databaseService.CopyToRoamingFolder();
        }

        private async void RetrieveDatabaseFromRoamingFolderExecute()
        {
            await databaseService.RetrieveFromRoamingFolder();
        }
    }
}
