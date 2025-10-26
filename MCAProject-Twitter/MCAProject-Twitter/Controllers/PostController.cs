using MCAProject_Twitter.CQRS.Commands;
using MCAProject_Twitter.CQRS.Queries;
using MCAProject_Twitter.DTOs;
using MCAProject_Twitter.Models;
using MCAProject_Twitter.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCAProject_Twitter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly CreatePostCommandHandler _createPostHandler;
        private readonly DeletePostCommandHandler _deletePostHandler;
        private readonly GetAllPostsQueryHandler _getAllPostsHandler;
        private readonly GetMyPostsQueryHandler _getMyPostsHandler;

        public PostsController(
            CreatePostCommandHandler createPostHandler,
            DeletePostCommandHandler deletePostHandler,
            GetAllPostsQueryHandler getAllPostsHandler,
            GetMyPostsQueryHandler getMyPostsHandler)
        {
            _createPostHandler = createPostHandler;
            _deletePostHandler = deletePostHandler;
            _getAllPostsHandler = getAllPostsHandler;
            _getMyPostsHandler = getMyPostsHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var query = new GetAllPostsQuery();
            var posts = await _getAllPostsHandler.Handle(query);
            return Ok(posts);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetMyPosts(string username)
        {
            var query = new GetMyPostsQuery { Username = username };
            var posts = await _getMyPostsHandler.Handle(query);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Content))
                return BadRequest("Content cannot be empty");

            var post = await _createPostHandler.Handle(command);
            return CreatedAtAction(nameof(GetAllPosts), new { id = post.Id }, post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var command = new DeletePostCommand { PostId = id };
            var success = await _deletePostHandler.Handle(command);

            if (!success)
                return NotFound($"Post with ID {id} not found");

            return NoContent();
        }
    }
}