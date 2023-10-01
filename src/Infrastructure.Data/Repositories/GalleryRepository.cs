using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class GalleryRepository : RepositoryBase<Gallery>, IGalleryRepository
    {
        private readonly AppDbContext _context;
        public GalleryRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
