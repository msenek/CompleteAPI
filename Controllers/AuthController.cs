using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Services;

namespace TestAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authservice;
        public AuthController(AuthService Authservice)
        {
            _authservice = Authservice;
        }

        [EnableRateLimiting("loginPolicy")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRequestDto dto)
        {
            var response = await _authservice.RegisterAsync(dto);
            return Ok(response);
        }

        [EnableRateLimiting("loginPolicy")]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserRequestDto dto)
        {
            var response = await _authservice.LoginAsync(dto);
            return Ok(response);
        }

        [EnableRateLimiting("loginPolicy")]
        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshAsync(RefreshToken token)
        {
            var response = await _authservice.RefreshAsync(token);
            return Ok(response);
        }
    }
}
