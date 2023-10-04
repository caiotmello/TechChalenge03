using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Test.Fixtures;
using Test.Infrastructure.Context;

namespace Test.API.Controllers
{
    [Collection(nameof(ArticleTestFixtureCollection))]
    public class ArticleControllerTest: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ArticleTestFixture _articleTestFixture;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private AppDbContext _context;

        public ArticleControllerTest(WebApplicationFactory<Program> factory, ArticleTestFixture articleTestFixture)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _articleTestFixture = articleTestFixture;
        }

        [Fact(DisplayName = "Should Retunr StatusCode Sucess")]
        [Trait("Category", "ArticleController Validation")]
        public async Task Get_GetAll_ReturnOkResultWithData()
        {
            _context = TestDatabaseInMemory.GetDatabase();
            var article = _articleTestFixture.GenerateArticle();
            _context.Articles.Add(article);
            _context.SaveChanges();

            var responce = await _client.GetAsync("/api/Article");
            var articles = await _client.GetFromJsonAsync<List<Article>>("/api/Article");
        }
    }
}
