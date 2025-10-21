using MCAProject_Twitter.Models;

namespace MCAProject_Twitter.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetByUsernameAsync(string username);
        Task<Post?> GetByIdAsync(int id);
        Task AddAsync(Post post);
        Task DeleteAsync(Post post);
        Task SaveChangesAsync();
    }
}
