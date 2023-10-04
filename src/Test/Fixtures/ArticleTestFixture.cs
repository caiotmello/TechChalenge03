using Bogus;
using Domain.Models;

namespace Test.Fixtures
{
    public class ArticleTestFixture
    {
        private readonly Faker _faker;

        public ArticleTestFixture()
        {
            _faker = new Faker();
        }

        public Article GenerateArticle()
        {
            var title = _faker.Random.String(20);
            var content = _faker.Random.String(100);
            var category = _faker.Random.String(10);
            var img = _faker.Internet.Url();
            var authorId = _faker.Random.Int(10);

            return new(title, content, category, img, authorId);
        }

        public Article GenerateArticleTitleEmpty()
        {
            var title = string.Empty;
            var content = _faker.Random.String(100);
            var category = _faker.Random.String(10);
            var img = _faker.Internet.Url();
            var authorId = _faker.Random.Int(10);

            return new(title, content, category, img, authorId);
        }

        public Article GenerateArticleContentEmpty()
        {
            var title = _faker.Random.String(20);
            var content = string.Empty;
            var category = _faker.Random.String(10);
            var img = _faker.Internet.Url();
            var authorId = _faker.Random.Int(10);

            return new(title, content, category, img, authorId);
        }

        public Article GenerateArticleAuthorIdEmpty()
        {
            var title = _faker.Random.String(20);
            var content = _faker.Random.String(100);
            var category = _faker.Random.String(10);
            var img = _faker.Internet.Url();
            var authorId = 0;

            return new(title, content, category, img, authorId);
        }

        public Article GenerateArticleTitleMaxLenght()
        {
            var title = _faker.Random.String(150);
            var content = _faker.Random.String(100);
            var category = _faker.Random.String(10);
            var img = _faker.Internet.Url();
            var authorId = _faker.Random.Int(10);

            return new(title, content, category, img, authorId);
        }
    }
}
