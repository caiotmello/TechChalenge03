namespace Application.Dtos.Request
{
    public class UpdateVideoRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Thumbnail { get; set; }
        public string UrlVideo { get; set; }
        public string? Slug { get; protected set; }
    }
}
