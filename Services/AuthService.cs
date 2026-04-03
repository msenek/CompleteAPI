using BCrypt.Net;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Repository;
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
                    Password = hash,
                    Role = "User"
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
               
                var token = _token.CreateToken(user);
                return token;
               }

        public async Task<RefreshToken> CreateRefToken(User user)
        {
            var RefreshToken = new RefreshToken()
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                IsRevoken = false,
                Expiration = DateTime.UtcNow.AddDays(7)
            };

            await _repository.CreateRefreshToken(RefreshToken);
            return RefreshToken; 
        }

        public async Task<AuthResponseDto> RefreshAsync(RefreshToken reftoken)
        {
            var token = await _repository.GetRefreshTokenAsync(reftoken);


            if (token == null)
            {
                throw new Exception("Login is required");
            }

            if (token.Expiration < DateTime.UtcNow || token.IsRevoken == true)
            {
                token.IsRevoken = true;
                await _repository.UpdateRefreshTokenAsync(token);
                throw new Exception("Login is required, the token expired");
            }

            token.IsRevoken = true;
            await _repository.UpdateRefreshTokenAsync(token);

            var user = await _repository.GetUserById(token.UserId);

            var newRefreshToken = await CreateRefToken(user);
            var newJWT = _token.CreateToken(user);

            return new AuthResponseDto()
            {
                RefreshToken = newRefreshToken.Token,
                JWT = newJWT,
            };


        }




    }
}
