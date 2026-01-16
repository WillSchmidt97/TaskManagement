using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Values.Objects;

namespace TaskManagement.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(long id, Include? includes = null);

        Task<T?> GetByIdAsync(long id, Include? includes = null);

        T? GetFirstOrDefault(Expression<Func<T, bool>> filter, Include? includes = null);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, Include? includes = null);

        List<T> GetAll(Expression<Func<T, bool>>? filter = null, Include? includes = null);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Include? includes = null);

        PagedResult<T> GetAllPaged(int page, int size, Expression<Func<T, bool>>? filter = null, Include? includes = null);

        Task<PagedResult<T>> GetAllPagedAsync(int page, int size, Expression<Func<T, bool>>? filter = null, Include? includes = null);

        Task<T> SaveAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
