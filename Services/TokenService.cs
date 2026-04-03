using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TestAPI.DB.Entities;


namespace TestAPI.Services
{
    public class TokenService
    {

        public string CreateToken(User user)
        {
            // clave
            string secretKey = "34759$%#@klVNJS3%0S@?/CDhujhRT$%^#q%4R";

            //claims
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(ClaimTypes.Role, user.Role),
            };
             
            var expiration = TimeSpan.FromHours(3);

            var token = GenerateJwtToken(secretKey, claims, expiration);
            return token;

        }

        //metodo
        public string GenerateJwtToken(string secretKey, IEnumerable<Claim> claims, TimeSpan expiration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                claims: claims,             
                expires: DateTime.UtcNow.Add(expiration), 
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }   
}
