using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MCAProject_Twitter.CQRS.Queries
{
    public class LoginQuery
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginQueryHandler
    {
        private readonly IUserRepository _repo;

        public LoginQueryHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> Handle(LoginQuery query)
        {
            var user = await _repo.GetByUsername(query.Username);
            if (user == null) return null;

            if (!VerifyPasswordHash(query.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(hash);
        }
    }
}
