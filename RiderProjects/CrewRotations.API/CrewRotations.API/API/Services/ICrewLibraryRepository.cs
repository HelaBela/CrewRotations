using System;
using System.Collections.Generic;
using CrewLibrary.API.Entities;
using CrewLibrary.API.ResourceParameters;

namespace CrewLibrary.API.Services
{
    public interface ICrewLibraryRepository
    {    
        IEnumerable<Crew> GetCrews(Guid authorId);
        Crew GetCourse(Guid authorId, Guid courseId);
        void AddCourse(Guid authorId, Crew crew);
        void UpdateCourse(Crew crew);
        void DeleteCourse(Crew crew);
        IEnumerable<Protege> GetAuthors();
        Protege GetAuthor(Guid authorId);
        IEnumerable<Protege> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Protege protege);
        void DeleteAuthor(Protege protege);
        void UpdateAuthor(Protege protege);
        bool ProtegeExists(Guid authorId);
        IEnumerable<Protege> GetAuthors(AuthorsResourceParameters authorsResource);
        bool Save();
    }
}
