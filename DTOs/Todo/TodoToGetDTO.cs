using TodoAppApi.Entity;

namespace TodoAppApi.DTOs
{
    public class TodoToGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public Category Category { get; set; } = new Category();
    }
}