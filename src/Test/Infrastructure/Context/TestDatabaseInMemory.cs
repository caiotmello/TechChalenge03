using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

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
    }
}
