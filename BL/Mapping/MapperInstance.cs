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
            .ForMember(target => target.Image, action => action.Ignore())
            .ForMember(target => target.ImageId, action => action.MapFrom(source => source.Image != null ? source.Image.Id : source.ImageId));

            cfg.CreateMap<DAL.Data.Image, Image>();
            cfg.CreateMap<Image, DAL.Data.Image>();

            cfg.CreateMap<DAL.Data.Category, Category>();
            cfg.CreateMap<Category, DAL.Data.Category>()
            .ForMember(target => target.Image, action => action.Ignore())
            .ForMember(target => target.ImageId, action => action.MapFrom(source => source.Image != null ? source.Image.Id : source.ImageId));

            cfg.CreateMap<FioSdkCsharp.Models.Info, BankAccountInfo>();

        }).CreateMapper();

    }
}
