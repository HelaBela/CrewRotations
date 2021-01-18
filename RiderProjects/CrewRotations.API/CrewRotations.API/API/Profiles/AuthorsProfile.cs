using AutoMapper;
using CrewLibrary.API.Helpers;

namespace CrewLibrary.API.Profiles
{
    public class AuthorsProfile: Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Entities.Protege, Models.AuthorDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom
                        (src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom
                        (src => src.DateOfBirth.GetCurrentAge()));

            CreateMap<Models.AuthorForCreationDto, Entities.Protege>();
        }

    }
}