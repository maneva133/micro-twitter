using MCAProject_Twitter.CQRS.Commands;
using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;
using Moq;
using Xunit;

namespace MCAProject_Twitter.Test
{
    public class DeletePostCommandHandlerTests
    {
        private readonly Mock<IPostRepository> _mockRepo;
        private readonly DeletePostCommandHandler _handler;

        public DeletePostCommandHandlerTests()
        {
            _mockRepo = new Mock<IPostRepository>();
            _handler = new DeletePostCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ExistingPost_ReturnsTrue()
        {
            var postId = 1;
            var post = new Post { Id = postId, Username = "user", Content = "content" };
            var command = new DeletePostCommand { PostId = postId };

            _mockRepo.Setup(r => r.GetByIdAsync(postId))
                .ReturnsAsync(post);
            _mockRepo.Setup(r => r.DeleteAsync(post))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command);

            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteAsync(post), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingPost_ReturnsFalse()
        {
            var command = new DeletePostCommand { PostId = 999 };
            _mockRepo.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Post?)null);

            var result = await _handler.Handle(command);

            Assert.False(result);
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Post>()), Times.Never);
        }
    }
}
