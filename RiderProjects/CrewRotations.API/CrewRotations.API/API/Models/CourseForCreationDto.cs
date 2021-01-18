using System;

namespace CrewLibrary.API.Models
{
    public class CourseForCreationDto
    {
        public string Title { get; set; }

      
        public string Description { get; set; }

   
        public Guid AuthorId { get; set; }
    }
}