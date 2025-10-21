using MCAProject_Twitter.Models;

namespace MCAProject_Twitter.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsername(string username);
        Task AddUser(User user);
    }
}
