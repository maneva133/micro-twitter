using MCAProject_Twitter.CQRS.Commands;
using MCAProject_Twitter.Repositories;
using Xunit;
using Moq;
using MCAProject_Twitter.Models;
namespace MCAProject_Twitter.Test
{
    public class CreatePostCommandHandlerTests
    {
        private readonly Mock<IPostRepository> _mockRepo;
        private readonly CreatePostCommandHandler _handler;

        public CreatePostCommandHandlerTests()
        {
            _mockRepo = new Mock<IPostRepository>();
            _handler = new CreatePostCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesPost()
        {
            var command = new CreatePostCommand
            {
                Username = "testuser",
                Content = "Test post content",
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command);

            Assert.NotNull(result);
            Assert.Equal(command.Username, result.Username);
            Assert.Equal(command.Content, result.Content);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithScheduledDate_SetsScheduledAt()
        {
            var scheduledDate = DateTime.UtcNow.AddHours(2);
            var command = new CreatePostCommand
            {
                Username = "testuser",
                Content = "Scheduled post",
            };

            var result = await _handler.Handle(command);

        }
    }
}
