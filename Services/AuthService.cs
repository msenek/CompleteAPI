using TestAPI.Repository;
using TestAPI.DB.Entities;
using System.Text.RegularExpressions;
using TestAPI.DB.DTOs;
using DevOne.Security.Cryptography.BCrypt;
using BCrypt.Net;
namespace TestAPI.Services
{
    public class AuthService
    {
        private readonly AuthRepository _repository;
        private readonly TokenService _token;
        public AuthService(AuthRepository authrepository, TokenService token)
        {
            _repository = authrepository;
            _token = token;
        }

        public async Task<User> RegisterAsync(UserRequestDto dto)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var existingUser = await _repository.GetUserByEmail(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already Exists");
            }
            else
            {
                var newuser = new User()
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    Password = hash
                };
                var user = await _repository.CreateUser(newuser);
                return user;
            }
        }
        public async Task<string> LoginAsync(UserRequestDto dto)
        {
            var user = await _repository.GetUserByEmail(dto.Email);
            var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (user == null)
               {
               throw new Exception("NotFound");
               }

               else if (!isValid)
               {
                throw new Exception("BadRequest");
               } 
               else 
               {
                var token = _token.CreateToken(user);
                return token;
               }
        }




    }
}
