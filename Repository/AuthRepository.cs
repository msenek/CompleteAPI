using TestAPI.DB;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Services;
using Microsoft.EntityFrameworkCore;
namespace TestAPI.Repository

{
    public class AuthRepository
    {
        private readonly TestAPIContext _context;
        public AuthRepository(TestAPIContext authcontext)
        {
            _context = authcontext;
        }


        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            return (user);

        }

        public async Task<User> CreateUser(User user)
        {
         var newuser = await _context.User.AddAsync(user);
            var saved = await _context.SaveChangesAsync();
            return user;

        }
    }
}
