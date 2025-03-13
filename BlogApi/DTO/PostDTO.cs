namespace BlogApi.DTO
{
    public class PostDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int CategoryId { get; set; }
        public int TagId { get; set; }
    }
}
