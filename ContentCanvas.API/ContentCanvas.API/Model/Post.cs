namespace ContentCanvas.API.Model
{
    public class Post
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Content { get; set; }
        public bool IsPublic { get; set; }
        public int UserId { get; set; }
    }
}
