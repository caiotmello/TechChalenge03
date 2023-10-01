using Domain.Validations;

namespace Domain.Models
{
    public class Gallery : BaseModel
    {
        public string Title { get; set; }
        public string Legend { get; set; }
        public string Thumbnail { get; set; }
        public IList<string> GalleryImages { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public Gallery(string title, string legend, string thumbnail, IList<string> galleryImages, int authorId)
        {
            Title = title;
            Legend = legend;
            Thumbnail = thumbnail;
            GalleryImages = galleryImages;
            AuthorId = authorId;
            PublishDate = DateTime.Now;
            ValidateEntity();
            
        }

        private void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "Title could not be empty!");
            AssertionConcern.AssertArgumentNotEmpty(Legend, "Legend could not be empty!");

            AssertionConcern.AssertArgumentLength(Title, 100, "The Title must be only 100 characters!");
            AssertionConcern.AssertArgumentLength(Legend, 50, "The Title must be only 100 characters!");
        }
    }
}
