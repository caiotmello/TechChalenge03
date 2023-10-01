namespace Application.Dtos.Request
{
    public class CreateVideoRequestDto
    {
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string UrlVideo { get; set; }
        public string? Slug { get; protected set; }
        public int AuthorId { get; set; }
    }
}
