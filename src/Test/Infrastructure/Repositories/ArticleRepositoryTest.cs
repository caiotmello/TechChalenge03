using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Test.Fixtures;
using Test.Infrastructure.Context;

namespace Test.Infrastructure.Repositories
{
    [Collection(nameof(ArticleTestFixtureCollection))]
    public class ArticleRepositoryTest
    {
        private readonly ArticleTestFixture _articleTestFixture;
        private AppDbContext _context;

        public ArticleRepositoryTest(ArticleTestFixture articleTestFixture)
        {
            _articleTestFixture = articleTestFixture;
        }

        [Fact(DisplayName = "Should add one article to Database")]
        [Trait("Category", "ArticleRepository Validation")]
        public async void AddAsync_ShouldSalveOneArticleOnDatabase()
        {
            _context = TestDatabaseInMemory.GetDatabase();
            var article = _articleTestFixture.GenerateArticle();
           
            var articleRepository = new ArticleRepository(_context);
            await articleRepository.AddAsync(article);

            var result = _context.Articles.FirstOrDefault();
            Assert.Equal(article.Content, result.Content);

        }

        [Fact(DisplayName = "Should return one article by Id")]
        [Trait("Category", "ArticleRepository Validation")]
        public async void GetAsync_ShouldReturnOneArticleById()
        {
            _context = TestDatabaseInMemory.GetDatabase();

            //MOK user on database
            var article = _articleTestFixture.GenerateArticle();
            _context.Articles.Add(article); 
            _context.SaveChanges();

            var articleRepository = new ArticleRepository(_context);
            var result = await articleRepository.GetAsync(1);

            Assert.Equal(article.Title,result.Title);

        }

    }
}
