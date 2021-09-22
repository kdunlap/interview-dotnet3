using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetAsync(long id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(long id);
    }
}