using AutoMapper;

namespace CrewLibrary.API.Profiles
{
    public class CoursesProfile: Profile
    {
        public CoursesProfile()
        {
            CreateMap<Entities.Crew, Models.CourseDto>();
            CreateMap<Models.CourseForCreationDto, Entities.Crew>();
        }
    }
}