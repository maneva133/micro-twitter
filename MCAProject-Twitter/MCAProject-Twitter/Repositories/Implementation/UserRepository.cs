using MCAProject_Twitter.Models;

namespace MCAProject_Twitter.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();

        public Task<User?> GetByUsername(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return Task.FromResult(user);
        }

        public Task AddUser(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
}
