using Domain.Models;
using System.Net;
using System.Net.Http.Json;
using Test.API;
using Test.Infrastructure;

namespace Test.IntegrationTest
{
    public class ArticleIntegrationTest
    {
        [Fact]
        public async Task GET_GetAll_ReturnAllArticles()
        {
            await using var application = new BlogApiApplication();

            await DatabaseMockData.CreateArticles(application, true);
            var url = "/api/Article";

            var client = application.CreateClient();

            var result = await client.GetAsync(url);
            var articles = await client.GetFromJsonAsync<List<Article>>("/api/Article");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(articles);
            Assert.True(articles.Count == 3);
        }
    }
}
