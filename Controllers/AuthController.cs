using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRequestDto dto)
        {
            var response = await _authservice.RegisterAsync(dto);
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserRequestDto dto)
        {
            var response = await _authservice.LoginAsync(dto);
            return Ok(response);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshAsync(RefreshToken token)
        {
            var response = await _authservice.RefreshAsync(token);
            return Ok(response);
        }
    }
}
