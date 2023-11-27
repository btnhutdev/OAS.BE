using AutoMapper;
using Core.ViewModel;
using Domain.Entities;

namespace Core.AutoMapperProfile
{
	public class ImageMapperConfig
	{
		public static Mapper InitAutomapper()
		{
			var config = new MapperConfiguration(config =>
			{
				config.CreateMap<ImageViewModel, Image>()
				.ForMember(dest => dest.IdProductNavigation, opt => opt.Ignore()) // Ignore the field
				.ReverseMap();

			});

			var mapper = new Mapper(config);
			return mapper;
		}
	}
}
