using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
