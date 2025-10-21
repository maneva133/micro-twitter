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
        private readonly PostService _service;
        public PostsController(PostService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllPosts());

        [HttpGet("{username}")]
        public async Task<IActionResult> GetMyPosts(string username) =>
            Ok(await _service.GetMyPosts(username));

        [HttpPost]
        public async Task<IActionResult> Create(PostDto dto)
        {
            if (dto.Content.Length < 12 || dto.Content.Length > 140)
                return BadRequest("Content must be 12-140 chars.");

            var post = new Post { Username = dto.Username, Content = dto.Content };
            await _service.CreatePost(post);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePost(id);
            return NoContent();
        }
    }
}
