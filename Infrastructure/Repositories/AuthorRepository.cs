using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
