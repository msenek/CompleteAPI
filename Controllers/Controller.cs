using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Services;
namespace TestAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TestApiController : ControllerBase
    {

        private readonly ProductService _service;
        public TestApiController(ProductService productservice)
        {
            _service = productservice;
        }

        [EnableRateLimiting("generalPolicy")]
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetAllAsync(int page, int pageSize, [FromQuery] FilteringDto filtering)
        {
            var response = await _service.GetAllAsync(page, pageSize, filtering);
            return Ok(response);
        }

        [EnableRateLimiting("generalPolicy")]
        [HttpGet("GetProductBy{Id}")]

        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var response = await _service.GetById(Id);

            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }
        [EnableRateLimiting("generalPolicy")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RequestDto request)
        {
            var product = await _service.CreateAsync(request);

            return Ok(product);
        }

        [EnableRateLimiting("generalPolicy")]
        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeProductBy{Id}")]

        public async Task<IActionResult> UpdateAsync(int Id, RequestDto request)
        {
            var product = await _service.UpdateAsync(Id, request);

            return Ok(product);
        }

        [EnableRateLimiting("generalPolicy")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProductBy{id}")]

        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var delete = await _service.DeleteAsync(Id);
            return Ok();
        }

    }
}