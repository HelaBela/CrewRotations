using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CrewLibrary.API.Helpers;
using CrewLibrary.API.Models;
using CrewLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrewLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly ICrewLibraryRepository _crewLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ICrewLibraryRepository crewLibraryRepository,
            IMapper mapper)
        {
            _crewLibraryRepository = crewLibraryRepository ??
                                       throw new ArgumentNullException(nameof(crewLibraryRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection(
            [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = _crewLibraryRepository.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorsToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(
            IEnumerable<AuthorForCreationDto> authorCollection)
        {
            var authorEntities = _mapper.Map<IEnumerable<Entities.Protege>>(authorCollection);
            foreach (var author in authorEntities)
            {
                _crewLibraryRepository.AddAuthor(author);
            }

            _crewLibraryRepository.Save();

            var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetAuthorCollection",
                new {ids = idsAsString},
                authorCollectionToReturn);
        }
    }
}