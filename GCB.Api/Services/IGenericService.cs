using System.Collections.Generic;
using System.Threading.Tasks;

namespace GCB.Api.Services

{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(params string[] includes);
        Task<T> GetByIdAsync(Guid id, params string[] includes);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(Guid id, T entity);
        Task<T> PatchAsync(Guid id, T entity);
        Task<bool> DeleteAsync(Guid id);
        
    }
}