using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;

namespace MCAProject_Twitter.CQRS.Queries
{
    public class GetAllPostsQuery { }

    public class GetAllPostsQueryHandler
    {
        private readonly IPostRepository _repo;

        public GetAllPostsQueryHandler(IPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Post>> Handle(GetAllPostsQuery query)
        {
            return await _repo.GetAllAsync();
        }
    }
}
