using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Test.Fixtures;
using Test.Infrastructure.Context;

namespace Test.IntegrationTest
{
    public class Teste : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private AppDbContext _context;

        public Teste(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task integration()
        {
            _context = TestDatabaseInMemory.GetDatabase();

            var article = new Article(title: "Article 1", content: "Content 1", category: "Category 1", img: "www.image1.com", authorId: 1);
            _context.Articles.Add(article);
            _context.SaveChanges();

            var responce = await _client.GetAsync("/api/Article");

        }
    }
}
