using MCAProject_Twitter.DTOs;
using MCAProject_Twitter.Models;
using MCAProject_Twitter.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCAProject_Twitter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _service.Register(new User { Username = dto.Username }, dto.Password);
            if (!success)
                return Conflict("Username already exists.");

            return Ok(new { message = "Registration successful." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _service.Login(dto.Username, dto.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(new { message = "Login successful", user = user.Username });
        }
    }
}
