using MCAProject_Twitter.Repositories;

namespace MCAProject_Twitter.CQRS.Commands
{
    public class DeletePostCommand
    {
        public int PostId { get; set; }
    }

    public class DeletePostCommandHandler
    {
        private readonly IPostRepository _repo;

        public DeletePostCommandHandler(IPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePostCommand command)
        {
            var post = await _repo.GetByIdAsync(command.PostId);
            if (post == null) return false;

            await _repo.DeleteAsync(post);
            return true;
        }
    }
}
