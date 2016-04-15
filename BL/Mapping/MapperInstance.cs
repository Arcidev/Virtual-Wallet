using AutoMapper;
using BL.Models;

namespace BL.Mapping
{
    public static class MapperInstance
    {
        public static IMapper Mapper { get; } = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.Data.Bank, Bank>().ConstructUsing(r => Bank.Create(((DAL.Data.Bank)r.SourceValue).Id));
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
