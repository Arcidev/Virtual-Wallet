using BL.Models;
using BL.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWallet.Controls;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;

namespace VirtualWallet.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private IDatabaseService databaseService;
        private IBankService bankService;
        private ICurrencyService currencyService;
        private string selectedLanguageCode;
        private ResourceLoader resources;
        private IList<LanguageInfo> availableLanguages;
        private IList<Currency> currencies;
        private Currency selectedCurrency;

        public ICommand RemoveAllDataCommand { get; private set; }

        public ICommand RemoveAllCredentialsCommand { get; private set; }

        public ICommand CopyDatabaseToRoamingFolderCommand { get; private set; }

        public ICommand RetrieveDatabaseFromRoamingFolderCommand { get; private set; }

        public IList<LanguageInfo> AvailableLanguages
        {
            get { return availableLanguages; }
            private set
            {
                if (availableLanguages == value)
                    return;

                availableLanguages = value;
                NotifyPropertyChanged();
            }
        }


        public Currency SelectedCurrency
        {
            get { return selectedCurrency; }
            set
            {
                if (selectedCurrency == value)
                    return;

                selectedCurrency = value;
                SaveCurrencies();
                NotifyPropertyChanged();
            }
        }

        public IList<Currency> Currencies
        {
            get { return currencies; }
            set
            {
                if (currencies == value)
                    return;

                currencies = value;
                NotifyPropertyChanged();
            }
        }

        public string Text_Application { get { return resources.GetString("Settings_Application"); } }

        public string Text_Language { get { return resources.GetString("Settings_Language"); } }

        public string Text_Header { get { return resources.GetString("Settings_Header"); } }

        public string Text_UserContent { get { return resources.GetString("Settings_UserContent"); } }

        public string Text_RemoveCredentials { get { return resources.GetString("Settings_RemoveCredentials"); } }

        public string Text_RemoveData { get { return resources.GetString("Settings_RemoveData"); } }

        public string Text_CopyDbToRoaming { get { return resources.GetString("Settings_CopyDbToRoaming"); } }

        public string Text_RetrieveDbFromRoaming { get { return resources.GetString("Settings_RetrieveDbFromRoaming"); } }

        public string Text_Currency { get { return resources.GetString("Settings_Currency"); } }
        

        public string SelectedLanguageCode
        {
            get { return selectedLanguageCode; }
            set
            {
                if (value == null)
                    return;

                selectedLanguageCode = value;
                NotifyPropertyChanged();
            }
        }

        public SettingsViewModel(IDatabaseService databaseService, IBankService bankService, ICurrencyService currencyService, ResourceLoader resources)
        {
            this.databaseService = databaseService;
            this.bankService = bankService;
            this.currencyService = currencyService;
            this.resources = resources;
            RemoveAllDataCommand = new CommandHandler(RemoveAllDataExecute);
            RemoveAllCredentialsCommand = new CommandHandler(RemoveAllCredentialsExecute);
            CopyDatabaseToRoamingFolderCommand = new CommandHandler(CopyDatabaseToRoamingFolderExecute);
            RetrieveDatabaseFromRoamingFolderCommand = new CommandHandler(RetrieveDatabaseFromRoamingFolderExecute);
        }

        public void ReloadTexts()
        {
            NotifyPropertyChanged(nameof(Text_Application));
            NotifyPropertyChanged(nameof(Text_CopyDbToRoaming));
            NotifyPropertyChanged(nameof(Text_Header));
            NotifyPropertyChanged(nameof(Text_Language));
            NotifyPropertyChanged(nameof(Text_RemoveCredentials));
            NotifyPropertyChanged(nameof(Text_RemoveData));
            NotifyPropertyChanged(nameof(Text_RetrieveDbFromRoaming));
            NotifyPropertyChanged(nameof(Text_UserContent));
            NotifyPropertyChanged(nameof(Text_Currency));

            LoadAvailableLanguages();
        }

        private void LoadAvailableLanguages()
        {
            var languages = ApplicationLanguages.ManifestLanguages.Select(x => new LanguageInfo() { DisplayName = new CultureInfo(x).NativeName, Code = x }).ToList();
            languages.Insert(0, new LanguageInfo() { DisplayName = resources.GetString("Settings_Language_None"), Code = "" });
            AvailableLanguages = languages;

            SelectedLanguageCode = ApplicationLanguages.PrimaryLanguageOverride;
        }

        private async void RemoveAllDataExecute()
        {
            await databaseService.RemoveAllDataAsync();
            RemoveAllCredentialsExecute();
        }

        private async void RemoveAllCredentialsExecute()
        {
            foreach (var bank in await bankService.GetAllAsync())
                bank.RemoveCredentials();
        }

        private async void CopyDatabaseToRoamingFolderExecute()
        {
            await databaseService.CopyToRoamingFolderAsync();
        }

        private async void RetrieveDatabaseFromRoamingFolderExecute()
        {
            await databaseService.RetrieveFromRoamingFolderAsync();
        }

        public async Task LoadDataAsync()
        {
            LoadAvailableLanguages();
            await LoadCurrencies();
        }

        private async Task LoadCurrencies()
        {
            Currencies = await currencyService.GetAllAsync();

            SelectedCurrency = Currencies.FirstOrDefault(x => x.IsDefaultCurrency);
        }

        private async void SaveCurrencies()
        {
            foreach (Currency c in Currencies)
                c.IsDefaultCurrency = c == SelectedCurrency;

            await currencyService.UpdateAsync(Currencies.ToArray());
        }
    }
}
