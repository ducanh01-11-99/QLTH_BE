using System.ComponentModel.DataAnnotations;

namespace QLTH.Models.DTO
{
    public class SchoolDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
