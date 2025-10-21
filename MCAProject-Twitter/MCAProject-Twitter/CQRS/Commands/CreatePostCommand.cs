using MCAProject_Twitter.DTOs;

namespace MCAProject_Twitter.CQRS.Commands
{
    public class CreatePostCommand
    {
        public PostDto PostDto { get; set; }
        public CreatePostCommand(PostDto postDto) { PostDto = postDto; }
    }
}
