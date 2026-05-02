using System.Security.Claims;
using TestAPI.DB.Entities;

namespace TestAPI.Services.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
        string GenerateJwtToken(string secretKey, IEnumerable<Claim> claims, TimeSpan expiration);

    }
}
