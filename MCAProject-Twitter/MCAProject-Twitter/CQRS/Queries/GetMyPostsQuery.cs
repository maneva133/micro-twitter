using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;

namespace MCAProject_Twitter.CQRS.Queries
{
    public class GetMyPostsQuery
    {
        public string Username { get; set; }
    }

    public class GetMyPostsQueryHandler
    {
        private readonly IPostRepository _repo;

        public GetMyPostsQueryHandler(IPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Post>> Handle(GetMyPostsQuery query)
        {
            return await _repo.GetByUsernameAsync(query.Username);
        }
    }
}
