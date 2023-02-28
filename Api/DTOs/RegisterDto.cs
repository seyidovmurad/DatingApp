using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateOnly? DateOfBirth { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string Password { get; set; }
    }
}