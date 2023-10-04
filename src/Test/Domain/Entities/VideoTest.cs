using Domain.Models;
using Domain.Validations;
using Test.Fixtures;

namespace Test.Domain.Entities
{
    public class VideoTest
    {
        [Fact(DisplayName ="Title should not be empty")]
        [Trait("Category", "Video Validation")]
        public void  VideoValidation_ShouldThrowException_WhenTitleIsEmpty()
        {
            var result = Assert.Throws<DomainValidationException>(() => 
                new Video(title: string.Empty,thumbnail:"teste", "www.teste.com",4 ));

            Assert.Equal("Title could not be empty!", result.Message);
        }

        [Fact(DisplayName = "AuthorId should not be empty")]
        [Trait("Category", "Video Validation")]
        public void VideoValidation_ShouldThrowException_WhenAuthorIdIsEmpty()
        {
            var result = Assert.Throws<DomainValidationException>(() =>
                new Video(title: "Title", thumbnail: "teste", "www.teste.com", 0));

            Assert.Equal("Author Id must be informed!", result.Message);
        }

        [Fact(DisplayName = "Title should not be more than 100 characters")]
        [Trait("Category", "Video Validation")]
        public void VideoValidation_ShouldThrowException_WhenTitleHasMaxThenLength()
        {
            var result = Assert.Throws<DomainValidationException>(() =>
                new Video(title: "title title title title title title title title title title title title title title title title title", thumbnail: "www.thumbnail.com", "www.teste.com", 4));

            Assert.Equal("The Title must be only 100 characters!", result.Message);
        }

        [Fact(DisplayName = "Create Video with success")]
        [Trait("Category", "Video Validation")]
        public void VideoValidation_ShouldCreateVideoWithSuccess()
        {
            var result = new Video(title: "title", thumbnail: "www.thumbnail.com", "www.teste.com", 4);

            Assert.NotNull(result);
        }
    }
}
