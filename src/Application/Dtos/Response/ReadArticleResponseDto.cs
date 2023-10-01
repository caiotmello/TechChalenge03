using System.Globalization;

namespace Application.Dtos.Response
{
    public class ReadArticleResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Img { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ReadAuthorResponseDto Author { get; set; }
    }
}
