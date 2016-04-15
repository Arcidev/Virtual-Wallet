using AutoMapper;
using BL.Models;

namespace BL.Mapping
{
    public static class MapperInstance
    {
        public static IMapper Mapper { get; } = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.Data.Bank, Bank>().ConstructUsing(r => Bank.Create(((DAL.Data.Bank)r.SourceValue).Id));
            cfg.CreateMap<Bank, DAL.Data.Bank>()
            .ForMember(target => target.Icon, action => action.Ignore())
            .ForMember(target => target.IconId, action => action.MapFrom(source => source.Icon != null ? source.Icon.Id : source.IconId));

            cfg.CreateMap<DAL.Data.Icon, Icon>();
            cfg.CreateMap<Icon, DAL.Data.Icon>();

            cfg.CreateMap<DAL.Data.Category, Category>();
            cfg.CreateMap<Category, DAL.Data.Category>()
            .ForMember(target => target.Icon, action => action.Ignore())
            .ForMember(target => target.IconId, action => action.MapFrom(source => source.Icon != null ? source.Icon.Id : source.IconId));

        }).CreateMapper();

    }
}
