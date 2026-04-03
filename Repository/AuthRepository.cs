using TestAPI.DB;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
namespace TestAPI.Repository

{
    public class AuthRepository
    {
        private readonly TestAPIContext _context;
        public AuthRepository(TestAPIContext authcontext)
        {
            _context = authcontext;
        }


        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            return (user);

        }


        public async Task<User?> GetUserById(int UserId)
        {
              var user = await _context.User.FindAsync(UserId);
            return user;
        }

        public async Task<User> CreateUser(User user)
        {
         var newuser = await _context.User.AddAsync(user);
            var saved = await _context.SaveChangesAsync();
                return user;

        }

        public async Task<RefreshToken> CreateRefreshToken(RefreshToken refresh)
        {
            var newRefToken = await _context.AddAsync(refresh);
            var saved = await _context.SaveChangesAsync();
            return refresh;
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken token)
        {
            _context.RefreshToken.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(RefreshToken token)
        {
            var find = await _context.RefreshToken.FindAsync(token.Token);
            return find;
        }
    }
}
