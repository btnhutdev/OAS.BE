using AutoMapper;
using Core.ViewModel;
using Domain.Entities;

namespace Core.AutoMapperProfile
{
    public class ProductMapperConfig
    {
        public static Mapper InitAutomapper()
        {
            var config = new MapperConfiguration(config =>
            {
                config.CreateMap<ImageViewModel, Image>();
                config.CreateMap<ProductViewModel, Product>()
                .ForMember(dest=> dest.Images, opt => opt.MapFrom(src=>src.Images))
                // Ignore the field
                .ForMember(dest => dest.Category, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auctions, opt => opt.Ignore())
                .ForMember(dest => dest.HistoryPayments, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ReverseMap();

            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}