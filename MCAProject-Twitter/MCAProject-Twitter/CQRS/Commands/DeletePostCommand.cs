namespace MCAProject_Twitter.CQRS.Commands
{
    public class DeletePostCommand
    {
        public int Id { get; set; }
        public DeletePostCommand(int id) { Id = id; }
    }
}
