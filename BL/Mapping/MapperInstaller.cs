using BL.Models;
using Mapster;
using Shared.Enums;

namespace BL.Mapping
{
    public static class MapperInstaller
    {
        public static void ConfigureMapper()
        {
            TypeAdapterConfig<DAL.Data.Bank, Bank>.NewConfig().ConstructUsing(x => Bank.Create(x.Id));
            TypeAdapterConfig<Bank, DAL.Data.Bank>.NewConfig()
                .Ignore(x => x.StoredTransactions)
                .Ignore(x => x.BankAccountInfo)
                .Ignore(x => x.Image)
                .Map(dest => dest.ImageId, src => src.Image != null ? src.Image.Id : src.ImageId);

            TypeAdapterConfig<Category, DAL.Data.Category>.NewConfig()
                .Ignore(x => x.Image)
                .Ignore(x => x.Wallets)
                .Ignore(x => x.Rules)
                .Map(dest => dest.ImageId, src => src.Image != null ? src.Image.Id : src.ImageId);

            TypeAdapterConfig<DAL.Data.Wallet, Wallet>.NewConfig()
                .Map(dest => dest.TimeRange, src => (TimeRange)src.TimeRangeId);
            TypeAdapterConfig<Wallet, DAL.Data.Wallet>.NewConfig()
                .Ignore(x => x.Image)
                .Map(dest => dest.ImageId, src => src.Image != null ? src.Image.Id : src.ImageId)
                .Map(dest => dest.TimeRangeId, src => (int)src.TimeRange);

            TypeAdapterConfig<WalletCategory, DAL.Data.WalletCategory>.NewConfig()
                .Ignore(x => x.Wallet)
                .Ignore(x => x.Category)
                .Map(dest => dest.WalletId, src => src.Wallet != null ? src.Wallet.Id : src.WalletId)
                .Map(dest => dest.CategoryId, src => src.Category != null ? src.Category.Id : src.CategoryId);

            TypeAdapterConfig<WalletBank, DAL.Data.WalletBank>.NewConfig()
                .Ignore(x => x.Wallet)
                .Ignore(x => x.Bank)
                .Map(dest => dest.WalletId, src => src.Wallet != null ? src.Wallet.Id : src.WalletId)
                .Map(dest => dest.BankId, src => src.Bank != null ? src.Bank.Id : src.BankId);

            TypeAdapterConfig<DAL.Data.Rule, Rule>.NewConfig()
                .Map(dest => dest.PatternType, src => (PatternType)src.PatternTypeId);
            TypeAdapterConfig<Rule, DAL.Data.Rule>.NewConfig()
                .Ignore(x => x.Category)
                .Map(dest => dest.PatternTypeId, src => (int)src.PatternType)
                .Map(dest => dest.CategoryId, src => src.Category != null ? src.Category.Id : src.CategoryId);
        }
    }
}
