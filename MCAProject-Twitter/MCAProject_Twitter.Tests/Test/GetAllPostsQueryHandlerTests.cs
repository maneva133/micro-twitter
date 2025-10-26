using MCAProject_Twitter.CQRS.Queries;
using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;
using Moq;
using Xunit;

namespace MCAProject_Twitter.Test
{
    public class GetAllPostsQueryHandlerTests
    {
        private readonly Mock<IPostRepository> _mockRepo;
        private readonly GetAllPostsQueryHandler _handler;

        public GetAllPostsQueryHandlerTests()
        {
            _mockRepo = new Mock<IPostRepository>();
            _handler = new GetAllPostsQueryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllPosts()
        {
            var posts = new List<Post>
            {
                new Post { Id = 1, Username = "user1", Content = "Post 1" },
                new Post { Id = 2, Username = "user2", Content = "Post 2" }
            };

            _mockRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(posts);

            var query = new GetAllPostsQuery();

            var result = await _handler.Handle(query);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}