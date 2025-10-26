using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MCAProject_Twitter.CQRS.Commands
{
    public class RegisterUserCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler
    {
        private readonly IUserRepository _repo;

        public RegisterUserCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<(bool Success, string Message)> Handle(RegisterUserCommand command)
        {
            if (await _repo.GetByUsername(command.Username) != null)
                return (false, "Username already exists");

            CreatePasswordHash(command.Password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                Username = command.Username,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _repo.AddUser(user);
            return (true, "User registered successfully");
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
