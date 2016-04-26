﻿using AutoMapper;
using BL.Models;

namespace BL.Mapping
{
    public static class MapperInstance
    {
        public static IMapper Mapper { get; } = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.Data.Bank, Bank>().ConstructUsing(r => Bank.Create(((DAL.Data.Bank)r.SourceValue).Id));
            cfg.CreateMap<Bank, DAL.Data.Bank>()
            .ForMember(target => target.Image, action => action.Ignore())
            .ForMember(target => target.ImageId, action => action.MapFrom(source => source.Image != null ? source.Image.Id : source.ImageId));

            cfg.CreateMap<DAL.Data.Image, Image>();
            cfg.CreateMap<Image, DAL.Data.Image>();

            cfg.CreateMap<DAL.Data.Category, Category>();
            cfg.CreateMap<Category, DAL.Data.Category>()
            .ForMember(target => target.Image, action => action.Ignore())
            .ForMember(target => target.ImageId, action => action.MapFrom(source => source.Image != null ? source.Image.Id : source.ImageId));

            cfg.CreateMap<FioSdkCsharp.Models.Info, BankAccountInfo>();
            cfg.CreateMap<DAL.Data.BankAccountInfo, BankAccountInfo>();
            cfg.CreateMap<BankAccountInfo, DAL.Data.BankAccountInfo>();

            cfg.CreateMap<DAL.Data.Wallet, Wallet>();
            cfg.CreateMap<Wallet, DAL.Data.Wallet>()
            .ForMember(target => target.Image, action => action.Ignore())
            .ForMember(target => target.ImageId, action => action.MapFrom(source => source.Image != null ? source.Image.Id : source.ImageId));

            cfg.CreateMap<DAL.Data.WalletCategory, WalletCategory>();
            cfg.CreateMap<WalletCategory, DAL.Data.WalletCategory>()
            .ForMember(target => target.Wallet, action => action.Ignore())
            .ForMember(target => target.WalletId, action => action.MapFrom(source => source.Wallet != null ? source.Wallet.Id : source.WalletId))
            .ForMember(target => target.Category, action => action.Ignore())
            .ForMember(target => target.CategoryId, action => action.MapFrom(source => source.Category != null ? source.Category.Id : source.CategoryId));

            cfg.CreateMap<DAL.Data.Rule, Rule>();
            cfg.CreateMap<Rule, DAL.Data.Rule>();

            cfg.CreateMap<DAL.Data.CategoryRule, CategoryRule>();
            cfg.CreateMap<CategoryRule, DAL.Data.CategoryRule>()
            .ForMember(target => target.Rule, action => action.Ignore())
            .ForMember(target => target.RuleId, action => action.MapFrom(source => source.Rule != null ? source.Rule.Id : source.RuleId))
            .ForMember(target => target.Category, action => action.Ignore())
            .ForMember(target => target.CategoryId, action => action.MapFrom(source => source.Category != null ? source.Category.Id : source.CategoryId));
        }).CreateMapper();
    }
}
