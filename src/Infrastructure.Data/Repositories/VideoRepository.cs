using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        private readonly AppDbContext _context;
        public VideoRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
