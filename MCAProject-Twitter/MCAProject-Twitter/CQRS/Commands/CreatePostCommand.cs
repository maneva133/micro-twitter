using MCAProject_Twitter.DTOs;
using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;

namespace MCAProject_Twitter.CQRS.Commands
{
    public class CreatePostCommand
    {
        public string Username { get; set; }
        public string Content { get; set; }
    }

    public class CreatePostCommandHandler
    {
        private readonly IPostRepository _repo;

        public CreatePostCommandHandler(IPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<Post> Handle(CreatePostCommand command)
        {
            var post = new Post
            {
                Username = command.Username,
                Content = command.Content,
                CreatedAt = DateTime.UtcNow,
            };

            await _repo.AddAsync(post);
            return post;
        }
    }
}
