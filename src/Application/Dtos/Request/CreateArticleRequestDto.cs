using System.Globalization;

namespace Application.Dtos.Request
{
    public class CreateArticleRequestDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Img { get; set; }
        public int AuthorId { get; set; }

        public CreateArticleRequestDto(string title, string content, string category, string img, int authorId)
        {
            Title = title;
            Content = content;
            Category = category;
            Img = img;
            AuthorId = authorId;
        }

    }
}
