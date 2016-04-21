using BL.Models;
using BL.Service;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using VirtualWallet.Controls;
using Windows.Globalization;

namespace VirtualWallet.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private IDatabaseService databaseService;
        private IBankService bankService;
        private string selectedLanguageCode;

        public ICommand RemoveAllDataCommand { get; private set; }

        public ICommand RemoveAllCredentialsCommand { get; private set; }

        public ICommand CopyDatabaseToRoamingFolderCommand { get; private set; }

        public ICommand RetrieveDatabaseFromRoamingFolderCommand { get; private set; }

        public IList<LanguageInfo> AvailableLanguages { get; private set; }

        public string SelectedLanguageCode
        {
            get { return selectedLanguageCode; }
            set
            {
                if (selectedLanguageCode == value)
                    return;

                selectedLanguageCode = value;
                NotifyPropertyChanged();
            }
        }

        public SettingsViewModel(IDatabaseService databaseService, IBankService bankService)
        {
            this.databaseService = databaseService;
            this.bankService = bankService;
            RemoveAllDataCommand = new CommandHandler(RemoveAllDataExecute);
            RemoveAllCredentialsCommand = new CommandHandler(RemoveAllCredentialsExecute);
            CopyDatabaseToRoamingFolderCommand = new CommandHandler(CopyDatabaseToRoamingFolderExecute);
            RetrieveDatabaseFromRoamingFolderCommand = new CommandHandler(RetrieveDatabaseFromRoamingFolderExecute);

            AvailableLanguages = ApplicationLanguages.ManifestLanguages.Select(x => new LanguageInfo() { DisplayName = new CultureInfo(x).DisplayName, Code = x }).ToList();
            SelectedLanguageCode = ApplicationLanguages.PrimaryLanguageOverride;
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
