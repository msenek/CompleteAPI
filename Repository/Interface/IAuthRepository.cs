using TestAPI.DB.Entities;

namespace TestAPI.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int UserId);
        Task<User> CreateUser(User user);
        Task<RefreshToken> CreateRefreshToken(RefreshToken refresh);
        Task UpdateRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(RefreshToken token);
    }
}
