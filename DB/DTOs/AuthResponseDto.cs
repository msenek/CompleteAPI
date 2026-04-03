using TestAPI.DB.Entities;

namespace TestAPI.DB.DTOs
{
    public class AuthResponseDto
    {
        public string JWT { get; set; }

        public string RefreshToken { get; set; }
    }
}
