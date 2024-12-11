using Core.Models;
using AutoMapper;
using Data.Enteties;

namespace Core.MapperProfiles
{
	public class AppProfile : Profile
	{
		public AppProfile() {
			CreateMap<AddAnimalModel, Animal>();
			CreateMap<Animal, AnimalModel>();
			CreateMap<Animal, EditAnimalModel>();
			CreateMap<EditAnimalModel, Animal>();
			CreateMap<RegisterModel, User>()
				.ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Email))
				.ForMember(x => x.PasswordHash, opt => opt.Ignore());
		}
	}
}
