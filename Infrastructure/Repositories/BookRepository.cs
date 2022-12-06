using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
