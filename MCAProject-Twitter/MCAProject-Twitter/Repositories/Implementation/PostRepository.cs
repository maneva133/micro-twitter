using MCAProject_Twitter.Data;
using MCAProject_Twitter.Models;
using Microsoft.EntityFrameworkCore;

namespace MCAProject_Twitter.Repositories.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Post>> GetAllAsync() =>
            await _context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();

        public async Task<IEnumerable<Post>> GetByUsernameAsync(string username) =>
            await _context.Posts.Where(p => p.Username == username).ToListAsync();

        public async Task<Post?> GetByIdAsync(int id) => await _context.Posts.FindAsync(id);

        public async Task AddAsync(Post post) { _context.Posts.Add(post); await SaveChangesAsync(); }

        public async Task DeleteAsync(Post post) { _context.Posts.Remove(post); await SaveChangesAsync(); }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
