using Domain.Validations;


namespace Domain.Models
{
    public class Video : BaseModel
    {
        public string Title { get; set; }
        public string? Thumbnail { get; set; } // É imagem reduzida do video
        public string UrlVideo { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public Video(string title, string thumbnail, string urlVideo, int authorId)
        {
            Title = title;
            Thumbnail = thumbnail;
            UrlVideo = urlVideo;
            AuthorId = authorId;
            PublishDate = DateTime.Now;

            ValidateEntity();
        }

        private void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "Title could not be empty!");

            AssertionConcern.AssertArgumentLength(Title, 100, "The Title must be only 100 characters!");


        }
    }
}
