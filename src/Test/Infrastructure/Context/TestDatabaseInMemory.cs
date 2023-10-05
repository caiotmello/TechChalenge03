using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Test.API;

namespace Test.Infrastructure.Context
{
    public class TestDatabaseInMemory
    {
        public static AppDbContext GetDatabase()
        {
            var name = Guid.NewGuid().ToString();
            return GetDatabase(name);
        }

        private static AppDbContext GetDatabase(string name)
        {
            var inMemoryOption = new DbContextOptionsBuilder<AppDbContext>()
                                .UseInMemoryDatabase(name).Options;

            return new AppDbContext(inMemoryOption);
        }

        public static AppDbContext GetDatabase(BlogApiApplication application)
        {
            var scope = application.Services.CreateScope();
            var provider = scope.ServiceProvider;
            var context = provider.GetRequiredService<AppDbContext>();
            
            context.Database.EnsureCreatedAsync();
           
            return context;
        }
    }
}
