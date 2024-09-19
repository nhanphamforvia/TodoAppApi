namespace TodoAppApi.DTOs
{
    public class TodoToUpdateDTO
    {
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
    }
}