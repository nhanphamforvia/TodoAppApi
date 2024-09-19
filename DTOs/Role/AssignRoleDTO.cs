using System.ComponentModel.DataAnnotations;

namespace TodoAppApi.DTOs
{
    public class AssignRoleDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}