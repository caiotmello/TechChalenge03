using Infrastructure.Data.Context;
using Infrastructure.Identity.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Test.Helpers;

namespace Test.API
{
    public class BlogApiApplication : WebApplicationFactory<Program>
    {
        public string DefaultUserId { get; set; } = "1";
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                services.RemoveAll(typeof(DbContextOptions<IdentityDataContext>));
                
               
                services.AddDbContext<AppDbContext>(options =>
                  options.UseInMemoryDatabase("BlogDb", root));

                //COnfigurations to bypass Authentication and Authorization
                services.Configure<TestAuthHandlerOptions>(options => options.DefaultUserId = DefaultUserId);

                services.AddDbContext<IdentityDataContext>(options =>
                    options.UseInMemoryDatabase("BlogDb", root));

            });

            return base.CreateHost(builder);
        }


        //Only for tests purposes
        static void ListAllServices (IServiceCollection services)
        {
           
            using (var writer = new StreamWriter("test.txt"))
            {
                writer.WriteLine("List of Registered Services:");
                foreach (var service in services)
                {
                    writer.WriteLine($"{service.ServiceType.FullName} -  {service.ImplementationType?.FullName}");
                }

                var test = services.FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            }
            
        }

    }
}
