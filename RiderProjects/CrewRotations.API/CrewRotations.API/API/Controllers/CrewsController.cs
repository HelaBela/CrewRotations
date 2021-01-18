using System;
using System.Collections.Generic;
using AutoMapper;
using CrewLibrary.API.Models;
using CrewLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrewLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CrewsController : ControllerBase
    {
        private ICrewLibraryRepository _crewLibraryRepository;
        private IMapper _mapper;


        public CrewsController(ICrewLibraryRepository crewLibraryRepository, IMapper mapper)
        {
            _crewLibraryRepository = crewLibraryRepository ??
                                       throw new ArgumentNullException(nameof(crewLibraryRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }
        //endpoint to get all crews accepting rotations
        
        
// Get crew by location will be queried from UI 
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCrewsProtegeRotatedTo(Guid authorId)
        {
            if (!_crewLibraryRepository.ProtegeExists(authorId))
            {
                return NotFound();
            }

            var coursesFromRepo = _crewLibraryRepository.GetCrews(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesFromRepo));
        }

        // we might not need this. Delete probably
        [HttpGet("{courseId}", Name ="GetCourseForAuthor")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_crewLibraryRepository.ProtegeExists(authorId))
            {
                return NotFound();
            }

            var courseFromRepo = _crewLibraryRepository.GetCourse(authorId, courseId);
            if (courseFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courseFromRepo));
        }
//create a team - will happen by adding a new team in database. so the below for now can be deleted. 
        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, CourseForCreationDto course)
        {
            if (!_crewLibraryRepository.ProtegeExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<CrewLibrary.API.Entities.Crew>(course);
            _crewLibraryRepository.AddCourse(authorId, courseEntity);
            _crewLibraryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            
            return CreatedAtRoute("GetCourseForAuthor", new
            {
                authorId = authorId,
                    courseId = courseToReturn.Id
            }, courseToReturn);
        }
    }
}