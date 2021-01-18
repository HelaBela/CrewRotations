using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrewLibrary.API.Entities
{
    public class Crew
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        [ForeignKey("AuthorId")]
        public Protege Protege { get; set; }

        public Guid AuthorId { get; set; }
    }
}
