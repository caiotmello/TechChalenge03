using Domain.Validations;
using Test.Fixtures;

namespace Test.Domain.Entities
{
    [Collection(nameof(AuthorTestFixtureCollection))]
    public class AuthorTest
    {
        private readonly AuthorTestFixture _authorTestFixture;

        public AuthorTest(AuthorTestFixture authorTestFixture)
        {
            _authorTestFixture = authorTestFixture;
        }

        [Fact(DisplayName = "Name should not be empty")]
        [Trait("Category", "Author Validation")]
        public void AuthorValidation_ShouldThrowException_WhenNameIsEmpty()
        {
            var result = Assert.Throws<DomainValidationException>(() => _authorTestFixture.GenerateAuthorNameEmpty());

            Assert.Equal("Name must be informed!", result.Message);
        }

        [Fact(DisplayName = "Email should not be empty")]
        [Trait("Category", "Author Validation")]
        public void AuthorValidation_ShouldThrowException_WhenEmailIsEmpty()
        {
            var result = Assert.Throws<DomainValidationException>(() => _authorTestFixture.GenerateAuthorEmailEmpty());

            Assert.Equal("Email must be informed!", result.Message);
        }

        [Fact(DisplayName = "Title should not be more than 50 characters")]
        [Trait("Category", "Author Validation")]
        public void AuthorValidation_ShouldThrowException_WhenTitleHasMaxThenLength()
        {
            var result = Assert.Throws<DomainValidationException>(() => _authorTestFixture.GenerateAuthorNameMaxLenght());

            Assert.Equal("The Name must be only 50 characteres!", result.Message);
        }

        [Fact(DisplayName = "Create Author with success")]
        [Trait("Category", "Author Validation")]
        public void AuthorValidation_ShouldCreateVideoWithSuccess()
        {
            var result = _authorTestFixture.GenerateAuthor();

            Assert.NotNull(result);
        }
    }
}
