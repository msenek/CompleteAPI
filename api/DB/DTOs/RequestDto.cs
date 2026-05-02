using System.ComponentModel.DataAnnotations;

namespace TestAPI.DB.DTOs
{
    public class RequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(50)]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
