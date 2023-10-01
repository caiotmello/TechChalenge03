using Domain.Validations;

namespace Domain.Models
{
    public class Article : BaseModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Img { get; set; }
        public string Category { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public Article(string title, string content,string img, string category, int authorId) 
        {
            Title = title;
            Content = content;
            Category = category;
            AuthorId = authorId;
            PublishDate = DateTime.Now;
            ValidateEntity();
        }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentFalse(string.IsNullOrEmpty(Title), "Name must be informed!");
            AssertionConcern.AssertArgumentFalse(string.IsNullOrEmpty(Content), "Content must be informed!");
            AssertionConcern.AssertArgumentFalse(AuthorId <= 0 , "Author Id must be informed!");

            AssertionConcern.AssertArgumentLength(Title, 100, "The Title must be only 100 characters!");
            
        }

    }
}