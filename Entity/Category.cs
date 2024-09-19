using System.ComponentModel.DataAnnotations;

namespace TodoAppApi.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}