using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Test.API;

namespace Test.Infrastructure
{
    public  class DatabaseMockData
    {
        public static async Task CreateArticles(BlogApiApplication application, bool create)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var context = provider.GetRequiredService<AppDbContext>())
                {
                    await  context.Database.EnsureCreatedAsync();

                    if (create)
                    {
                        await context.Authors.AddAsync(new Author(name:"Name 1", email: "email1@email.com"));
                        await context.Articles.AddAsync(new Article(title: "Article 1",content: "Content 1",category: "Category 1",img: "www.image1.com",authorId: 1 ));
                        await context.Articles.AddAsync(new Article(title: "Article 2", content: "Content 2", category: "Category 2", img: "www.image2.com", authorId: 1));
                        await context.Articles.AddAsync(new Article(title: "Article 3", content: "Content 3", category: "Category 3", img: "www.image3.com", authorId: 1));
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
