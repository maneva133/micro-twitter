using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;

namespace MCAProject_Twitter.Services
{
    public class PostService
    {
        private readonly IPostRepository _repo;
        public PostService(IPostRepository repo) { _repo = repo; }

        public async Task<IEnumerable<Post>> GetAllPosts() => await _repo.GetAllAsync();

        public async Task<IEnumerable<Post>> GetMyPosts(string username) =>
            await _repo.GetByUsernameAsync(username);

        public async Task<Post> CreatePost(Post post)
        {
            await _repo.AddAsync(post);
            return post;
        }

        public async Task DeletePost(int id)
        {
            var post = await _repo.GetByIdAsync(id);
            if (post != null) await _repo.DeleteAsync(post);
        }
    }
}
