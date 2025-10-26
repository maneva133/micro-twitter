using MCAProject_Twitter.CQRS.Commands;
using MCAProject_Twitter.CQRS.Queries;
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
        private readonly RegisterUserCommandHandler _registerHandler;
        private readonly LoginQueryHandler _loginHandler;

        public UsersController(
            RegisterUserCommandHandler registerHandler,
            LoginQueryHandler loginHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Username) ||
                string.IsNullOrWhiteSpace(command.Password))
                return BadRequest("Username and password are required");

            var (success, message) = await _registerHandler.Handle(command);

            if (!success)
                return BadRequest(message);

            return Ok(new { message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Username) ||
                string.IsNullOrWhiteSpace(query.Password))
                return BadRequest("Username and password are required");

            var user = await _loginHandler.Handle(query);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { message = "Login successful", user = user.Username });
        }
    }
}
