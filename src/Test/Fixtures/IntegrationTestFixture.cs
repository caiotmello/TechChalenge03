using Bogus;
using Infrastructure.Data.Context;
using Test.Infrastructure.Context;
using Test.API;
using Domain.Models;
using Application.Dtos.Request;

namespace Test.Fixtures
{
    public class IntegrationTestFixture
    {
        private readonly Faker _faker;
        public AppDbContext _context;
        public HttpClient Client;
        public BlogApiApplication application;

        public IntegrationTestFixture()
        {
            application =  new BlogApiApplication();
            Client = application.CreateClient();
            _faker = new Faker();
            _context = TestDatabaseInMemory.GetDatabase(application);
        }

        public async Task FillDatabaseAsync()
        {
            await _context.Authors.AddAsync(new Author(name: "Name 1", email: "email1@email.com"));
            await _context.Authors.AddAsync(new Author(name: "Name 2", email: "email2@email.com"));
            await _context.Articles.AddAsync(new Article(title: "Article 1", content: "Content 1", category: "Category 1", img: "www.image1.com", authorId: 1));
            await _context.Articles.AddAsync(new Article(title: "Article 2", content: "Content 2", category: "Category 2", img: "www.image2.com", authorId: 2));
            await _context.Articles.AddAsync(new Article(title: "Article 3", content: "Content 3", category: "Category 3", img: "www.image3.com", authorId: 1));
            await _context.Videos.AddAsync(new Video(title: "Title 1", thumbnail: "Thumbnail 1", urlVideo: "www.video1.com.br", authorId: 2));
            await _context.Videos.AddAsync(new Video(title: "Title 2", thumbnail: "Thumbnail 2", urlVideo: "www.video2.com.br", authorId: 1));
            await _context.SaveChangesAsync();
        }

        public CreateArticleRequestDto CreateArticleRequestDto()
        {
            var title = _faker.Random.String(20);
            var content = _faker.Random.String(100);
            var category = _faker.Random.String(10);
            var img = _faker.Internet.Url();
            var authorId = _faker.Random.Int(10);

            return new(title, content, category, img, authorId);
        }
        public CreateArticleRequestDto CreateArticleRequestDtoEmptyTitle()
        {
            var title = string.Empty;
            var content = _faker.Random.String(100);
            var category = _faker.Random.String(10);
            var img = _faker.Internet.Url();
            var authorId = _faker.Random.Int(10);

            return new(title, content, category, img, authorId);
        }


    }
}