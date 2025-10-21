using System.ComponentModel.DataAnnotations;

namespace MCAProject_Twitter.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
