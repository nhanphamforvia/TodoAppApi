namespace TodoAppApi.DTOs
{
    public class TodoToCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}