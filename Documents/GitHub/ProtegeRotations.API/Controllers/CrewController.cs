using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProtegeRotations.API.Models;
using ProtegeRotations.API.Services;

namespace ProtegeRotations.API.Controllers
{
    [ApiController]
    [Route("api/proteges/{lastName}/crews")]
    public class CrewController : ControllerBase
    {
        private readonly ICrewLibraryRepository _crewLibraryRepository;
        private readonly IMapper _mapper;

        public CrewController(ICrewLibraryRepository crewLibraryRepository, IMapper mapper)
        {
            _crewLibraryRepository = crewLibraryRepository ?? 
                                     throw new ArgumentNullException(nameof(crewLibraryRepository));
            _mapper = mapper ?? 
                      throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<CrewDto>> GetCrewsForProtege(string lastName)
        {
            if (!_crewLibraryRepository.ProtegeExists(lastName))
            {
                return NotFound();
            }

            var crewsByProtegeLastNameFromRepo = _crewLibraryRepository.GetCrews(lastName);
            return Ok(_mapper.Map<IEnumerable<CrewDto>>(crewsByProtegeLastNameFromRepo));
        }
    }
}