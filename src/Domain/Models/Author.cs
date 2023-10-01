using Domain.Validations;

namespace Domain.Models
{
    public class Author : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        //public virtual ICollection<Gallery> Galleries { get; set; }

        public Author(string name, string email)
        {
            Name = name;
            Email = email;
            PublishDate = DateTime.Now;
            ValidateEntity();
        }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentFalse(string.IsNullOrEmpty(Name), "Name must be informed!");
            AssertionConcern.AssertArgumentFalse(string.IsNullOrEmpty(Email), "Email must be informed!");

            AssertionConcern.AssertArgumentLength(Name, 50, "The Name must be only 50 characteres!");

        }
    }
}