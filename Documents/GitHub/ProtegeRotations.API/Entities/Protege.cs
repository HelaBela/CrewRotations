using System;
using System.ComponentModel.DataAnnotations;

namespace ProtegeRotations.API.Entities
{
    public class Protege
    {
        [Key]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
    }
}