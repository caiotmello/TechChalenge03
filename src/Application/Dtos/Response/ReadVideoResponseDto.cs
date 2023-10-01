namespace Application.Dtos.Response
{
    public class ReadVideoResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string UrlVideo { get; set; }
        public string Slug { get; protected set; }
        public DateTime PublishDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ReadAuthorResponseDto Author { get; set; }
    }
}
