using System;
using System.Collections.Generic;
using System.Linq;
using CrewLibrary.API.DbContexts;
using CrewLibrary.API.Entities;
using CrewLibrary.API.ResourceParameters;

namespace CrewLibrary.API.Services
{
    public class CrewLibraryRepository : ICrewLibraryRepository, IDisposable
    {
        private readonly CrewLibraryContext _context;

        public CrewLibraryRepository(CrewLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //I will need another context that will lead to the other table - Protege. This wil be in another file 
        }

        public void AddCourse(Guid authorId, Crew crew)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (crew == null)
            {
                throw new ArgumentNullException(nameof(crew));
            }

            // always set the AuthorId to the passed-in authorId
            crew.AuthorId = authorId;
            _context.Crews.Add(crew);
        }

        public void DeleteCourse(Crew crew)
        {
            _context.Crews.Remove(crew);
        }

        public Crew GetCourse(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Crews
                .Where(c => c.AuthorId == authorId && c.Id == courseId).FirstOrDefault();
        }

        //this will be getTeams with no parameters
        public IEnumerable<Crew> GetCrews(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Crews
                //.Where(c => c.AuthorId == authorId)
                //ordered by location (melb)
                .OrderBy(c => c.Title).ToList();
        }

        public void UpdateCourse(Crew crew)
        {
            // no code in this implementation
        }

        public void AddAuthor(Protege protege)
        {
            if (protege == null)
            {
                throw new ArgumentNullException(nameof(protege));
            }

            // the repository fills the id (instead of using identity columns)
            protege.Id = Guid.NewGuid();

            foreach (var course in protege.Courses)
            {
                course.Id = Guid.NewGuid();
            }

            _context.Proteges.Add(protege);
        }

        public bool ProtegeExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Proteges.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Protege protege)
        {
            if (protege == null)
            {
                throw new ArgumentNullException(nameof(protege));
            }

            _context.Proteges.Remove(protege);
        }

        public Protege GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Proteges.FirstOrDefault(a => a.Id == authorId);
        }

        public IEnumerable<Protege> GetAuthors()
        {
            return _context.Proteges.ToList<Protege>();
        }

        public IEnumerable<Protege> GetAuthors(AuthorsResourceParameters authorsResource)
        {
            if (authorsResource == null)
            {
                throw new ArgumentNullException(nameof(authorsResource));
            }

            if (string.IsNullOrWhiteSpace(authorsResource.MainCategory) &&
                string.IsNullOrWhiteSpace(authorsResource.SearchQuery))
            {
                return GetAuthors();
            }

            var collection = _context.Proteges as IQueryable<Protege>;

            if (!string.IsNullOrWhiteSpace(authorsResource.MainCategory))
            {
                var mainCategory = authorsResource.MainCategory.Trim();
                collection = collection.Where(a => a.MainCategory == authorsResource.MainCategory);
            }

            if (!string.IsNullOrWhiteSpace(authorsResource.SearchQuery))
            {
                var searchQuery = authorsResource.SearchQuery.Trim();
                collection = collection.Where(a => a.MainCategory.Contains(searchQuery)
                                                   || a.FirstName.Contains(searchQuery)
                                                   || a.LastName.Contains(searchQuery));
            }

            return collection.ToList();
        }

        public IEnumerable<Protege> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return _context.Proteges.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public void UpdateAuthor(Protege protege)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}