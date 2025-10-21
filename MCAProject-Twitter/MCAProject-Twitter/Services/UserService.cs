using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MCAProject_Twitter.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Register(User user, string password)
        {
            if (await _repo.GetByUsername(user.Username) != null)
                return false; 

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            await _repo.AddUser(user);
            return true;
        }

        public async Task<User?> Login(string username, string password)
        {
            var user = await _repo.GetByUsername(username);
            if (user == null) return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;
            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(hash);
        }
    }
}
