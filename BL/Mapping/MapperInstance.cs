using AutoMapper;
using BL.Models;
using BL.Models.BankModels;
using System;

namespace BL.Mapping
{
    public static class MapperInstance
    {
        public static IMapper Mapper { get; } = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.Data.Bank, Bank>().ConstructUsing((Func<ResolutionContext, Bank>)(r => new Fio()));
            cfg.CreateMap<Bank, DAL.Data.Bank>();

            cfg.CreateMap<DAL.Data.Icon, Icon>();
            cfg.CreateMap<Icon, DAL.Data.Icon>();

            cfg.CreateMap<DAL.Data.Category, Category>();
            cfg.CreateMap<Category, DAL.Data.Category>();

            cfg.CreateMap<DAL.Data.Icon, Icon>();
            cfg.CreateMap<Icon, DAL.Data.Icon>();

        }).CreateMapper();

    }
}
