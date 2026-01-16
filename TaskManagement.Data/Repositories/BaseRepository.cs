using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using TaskManagement.Data.Extensions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Domain.Values.Objects;

namespace TaskManagement.Data.Repositories
{
    public class BaseRepository<TContext, T>(TContext context) : IBaseRepository<T> where TContext : DbContext where T : Entity
    {
        protected readonly TContext Context = context;
        protected readonly DbSet<T> DbSet = context.Set<T>();

        public T? GetById(long id, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);
            return query.FirstOrDefault(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(long id, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);
            return query.FirstOrDefault(filter);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);
            return await query.FirstOrDefaultAsync(filter);
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public PagedResult<T> GetAllPaged(int page, int size, Expression<Func<T, bool>>? filter = null, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);

            if (filter != null)
                query = query.Where(filter);

            return query.OrderByDescending(u => u.Id).ToPagedResult(page, size);
        }

        public async Task<PagedResult<T>> GetAllPagedAsync(int page, int size, Expression<Func<T, bool>>? filter = null, Include? includes = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();
            query = query.AddInclude(includes);

            if (filter != null)
                query = query.Where(filter);

            return await query.OrderByDescending(u => u.Id).ToPagedResultAsync(page, size);
        }

        public async Task<T> SaveAsync(T entity)
        {
            if (entity.Id > 0)
                throw new ValidationException("O ID não pode ser informado na criação de um novo objeto!");

            entity.CreatedAt = DateTime.Now;

            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity.Id <= 0)
                throw new ValidationException("ID inválido!");

            entity.UpdatedAt = DateTime.Now;

            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public void Add(T entity)
        {
            if (entity.Id > 0)
                throw new ValidationException("O ID não pode ser informado na criação de um novo objeto!");

            entity.CreatedAt = DateTime.Now;

            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity.Id <= 0)
                throw new ValidationException("ID inválido!");

            entity.UpdatedAt = DateTime.Now;

            DbSet.Update(entity);
        }

        public void Delete(T entity) => DbSet.Remove(entity);

        public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();
    }
}
