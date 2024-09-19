using System.ComponentModel.DataAnnotations;

namespace TodoAppApi.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
    }
}