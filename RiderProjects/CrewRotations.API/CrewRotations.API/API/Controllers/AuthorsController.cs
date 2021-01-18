using System;
using System.Collections.Generic;
using AutoMapper;
using CrewLibrary.API.Helpers;
using CrewLibrary.API.Models;
using CrewLibrary.API.ResourceParameters;
using CrewLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrewLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private ICrewLibraryRepository _crewLibraryRepository;
        private IMapper _mapper;

        public AuthorsController(ICrewLibraryRepository crewLibraryRepository, IMapper mapper)
        {
            _crewLibraryRepository = crewLibraryRepository ??
                                       throw new ArgumentNullException(nameof(crewLibraryRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorsResourceParameters authorsResource)
        {
            //throw new Exception("testing");
            var authorsFromRepo = _crewLibraryRepository.GetAuthors(authorsResource);

            return new JsonResult(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _crewLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            var authorEntity = _mapper.Map<Entities.Protege>(author);
            _crewLibraryRepository.AddAuthor(authorEntity);
            _crewLibraryRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new
            {
                authorId = authorToReturn.Id
            }, authorToReturn);

        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
    }