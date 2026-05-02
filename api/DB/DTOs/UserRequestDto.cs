using System.ComponentModel.DataAnnotations;

namespace TestAPI.DB.DTOs
{
    public class UserRequestDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]      
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string Password { get; set; }

    }
}
