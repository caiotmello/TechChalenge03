using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Video> Videos { get; set; }

        //public DbSet<Gallery> Gallerys { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Article>()
                .HasOne(article => article.Author)
                .WithMany(author => author.Articles)
                .HasForeignKey(article => article.AuthorId);


            builder.Entity<Video>()
                .HasOne(video => video.Author)
                .WithMany(author => author.Videos)
                .HasForeignKey(video => video.AuthorId);

            /*builder.Entity<Gallery>()
               .HasOne(gallery => gallery.Author)
               .WithMany(author => author.Galleries)
               .HasForeignKey(gallery => gallery.AuthorId);
            */
            //base.OnModelCreating(builder);
        }
    }
}
