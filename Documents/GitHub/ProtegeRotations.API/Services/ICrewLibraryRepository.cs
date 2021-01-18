using System.Collections;
using System.Collections.Generic;
using ProtegeRotations.API.Entities;

namespace ProtegeRotations.API.Services
{
    public interface ICrewLibraryRepository
    {
        IEnumerable<Crew> GetCrews(string protegeLastName);
        bool ProtegeExists(string lastName);

    }
}