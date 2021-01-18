using System;
using System.Collections.Generic;
using System.Linq;
using ProtegeRotations.API.DbContexts;
using ProtegeRotations.API.Entities;

namespace ProtegeRotations.API.Services
{
    public class CrewLibraryRepository : ICrewLibraryRepository
    {
        private readonly CrewLibraryContext _context;

        public CrewLibraryRepository(CrewLibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Crew> GetCrews(string protegeLastName)
        {
            if (protegeLastName == string.Empty)
            {
                throw new ArgumentNullException(nameof(protegeLastName));
            }

            return _context.Crews.Where(c => c.ProtegeSurname == protegeLastName)
                .OrderBy(c => c.Name).ToList();
        }
        
        public bool ProtegeExists(string lastName)
        {
            if (lastName == string.Empty)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            return _context.Proteges.Any(a => a.LastName == lastName);
        }
    }
}