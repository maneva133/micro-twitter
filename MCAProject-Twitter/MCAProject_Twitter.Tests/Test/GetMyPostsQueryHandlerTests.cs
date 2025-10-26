using MCAProject_Twitter.CQRS.Queries;
using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;
using Moq;
using Xunit;

namespace MCAProject_Twitter.Test
{
    public class GetMyPostsQueryHandlerTests
    {
        private readonly Mock<IPostRepository> _mockRepo;
        private readonly GetMyPostsQueryHandler _handler;

        public GetMyPostsQueryHandlerTests()
        {
            _mockRepo = new Mock<IPostRepository>();
            _handler = new GetMyPostsQueryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ReturnsUserPosts()
        {
            var username = "testuser";
            var posts = new List<Post>
            {
                new Post { Id = 1, Username = username, Content = "My post 1" },
                new Post { Id = 2, Username = username, Content = "My post 2" }
            };

            _mockRepo.Setup(r => r.GetByUsernameAsync(username))
                .ReturnsAsync(posts);

            var query = new GetMyPostsQuery { Username = username };

            var result = await _handler.Handle(query);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, p => Assert.Equal(username, p.Username));
        }
    }
}
