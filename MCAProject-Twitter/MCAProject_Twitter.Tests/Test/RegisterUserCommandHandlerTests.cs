using MCAProject_Twitter.CQRS.Commands;
using MCAProject_Twitter.Models;
using MCAProject_Twitter.Repositories;
using Moq;
using Xunit;

namespace MCAProject_Twitter.Test
{
    public class RegisterUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserCommandHandlerTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _handler = new RegisterUserCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_NewUser_ReturnsSuccess()
        {
            var command = new RegisterUserCommand
            {
                Username = "newuser",
                Password = "Password123!"
            };

            _mockRepo.Setup(r => r.GetByUsername(command.Username))
                .ReturnsAsync((User?)null);
            _mockRepo.Setup(r => r.AddUser(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var (success, message) = await _handler.Handle(command);

            Assert.True(success);
            Assert.Equal("User registered successfully", message);
            _mockRepo.Verify(r => r.AddUser(It.Is<User>(u =>
                u.Username == command.Username &&
                u.PasswordHash != null &&
                u.PasswordSalt != null)), Times.Once);
        }

        [Fact]
        public async Task Handle_ExistingUsername_ReturnsFailure()
        {
            var command = new RegisterUserCommand
            {
                Username = "existinguser",
                Password = "Password123!"
            };

            var existingUser = new User { Username = command.Username };
            _mockRepo.Setup(r => r.GetByUsername(command.Username))
                .ReturnsAsync(existingUser);

            var (success, message) = await _handler.Handle(command);

            Assert.False(success);
            Assert.Equal("Username already exists", message);
            _mockRepo.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
        }
    }
}
