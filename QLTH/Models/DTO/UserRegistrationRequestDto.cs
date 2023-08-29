using System.ComponentModel.DataAnnotations;

namespace QLTH.Models.DTO
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string Name {  get; set; }
        public string Email {  get; set; }
        [Required]
        public string Password {  get; set; }
    }
}
