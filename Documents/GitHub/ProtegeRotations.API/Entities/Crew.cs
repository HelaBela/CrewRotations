using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtegeRotations.API.Entities
{
    public class Crew
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(1500)]
        public string Description { get; set; }
        
        [ForeignKey("ProtegeSurname")]
        public Protege Protege { get; set; }
        
        public string ProtegeSurname { get; set; }
    }
}