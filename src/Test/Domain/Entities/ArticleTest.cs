using Domain.Validations;
using Test.Fixtures;

namespace Test.Domain.Entities
{
    [Collection(nameof(ArticleTestFixtureCollection))]
    public class ArticleTest
    {
        private readonly ArticleTestFixture _articleTestFixture;

        public ArticleTest(ArticleTestFixture articleTestFixture)
        {
            _articleTestFixture = articleTestFixture;
        }

        [Fact(DisplayName = "[UnitTest] Article - Title should not be empty")]
        [Trait("Category", "Article Validation")]
        public void ArticleValidation_ShouldThrowException_WhenTitleIsEmpty()
        {
            var result = Assert.Throws<DomainValidationException>(() => _articleTestFixture.GenerateArticleTitleEmpty());

            Assert.Equal("Title must be informed!", result.Message);
        }

        [Fact(DisplayName = "[UnitTest] Article - Content should not be empty")]
        [Trait("Category", "Article Validation")]
        public void ArticleValidation_ShouldThrowException_WhenContentIsEmpty()
        {
            var result = Assert.Throws<DomainValidationException>(() => _articleTestFixture.GenerateArticleContentEmpty());

            Assert.Equal("Content must be informed!", result.Message);
        }

        [Fact(DisplayName = "[UnitTest] Article - AuthorId should not be empty")]
        [Trait("Category", "Article Validation")]
        public void ArticleValidation_ShouldThrowException_WhenAuthorIdIsEmpty()
        {
            var result = Assert.Throws<DomainValidationException>(() => _articleTestFixture.GenerateArticleAuthorIdEmpty());

            Assert.Equal("Author Id must be informed!", result.Message);
        }

        [Fact(DisplayName = "[UnitTest] Article - Title should not be more than 100 characters")]
        [Trait("Category", "Article Validation")]
        public void ArticleValidation_ShouldThrowException_WhenTitleHasMaxThenLength()
        {
            var result = Assert.Throws<DomainValidationException>(() => _articleTestFixture.GenerateArticleTitleMaxLenght());

            Assert.Equal("The Title must be only 100 characters!", result.Message);
        }

        [Fact(DisplayName = "[UnitTest] Article - Create Article with success")]
        [Trait("Category", "Article Validation")]
        public void ArticleValidation_ShouldCreateVideoWithSuccess()
        {
            var result = _articleTestFixture.GenerateArticle();

            Assert.NotNull(result);
        }

    }
}
