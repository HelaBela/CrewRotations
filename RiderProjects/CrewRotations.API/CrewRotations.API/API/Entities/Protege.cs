using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrewLibrary.API.Entities
{
    public class Protege
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }          

        [Required]
        [MaxLength(50)]
        public string MainCategory { get; set; }

        public ICollection<Crew> Courses { get; set; }
            = new List<Crew>();
    }
}
