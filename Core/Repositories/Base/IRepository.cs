using Core.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories.Base
{
    public interface IRepository <T> where T : Entity
    {
        IQueryable<T> GetQuery();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task Commit();
    }
}
