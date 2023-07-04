using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Test.Data;

namespace Test.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAsync();
        }

        public virtual async Task<T?> Get<Tkey>(Tkey id, bool eager = false, params Expression<Func<T, object>>[] props)
        {
            if (eager)
            {
                var data = _context.Set<T>().AsNoTracking().AsQueryable();
                var entityType = _context.Model.FindEntityType(typeof(T));
                foreach (var property in entityType?.GetNavigations() ?? Enumerable.Empty<INavigation>())
                    data = data.Include(property.Name);
                var idProperty = entityType?.FindPrimaryKey()?.Properties.FirstOrDefault()
                    ?? throw new InvalidOperationException($"Entity {typeof(T).Name} does not have a primary key.");
                var parameter = Expression.Parameter(typeof(T));
                var predicate = Expression.Equal(Expression.Property(parameter, idProperty.Name), Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                return await data.FirstOrDefaultAsync(lambda);
            }
            if (props.Length > 0)
            {
                var query = _context.Set<T>().AsNoTracking().Include(props.First());
                foreach (var path in props.Skip(1))
                {
                    query = query.Include(path);
                }

                var idProperty = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.FirstOrDefault()
                    ?? throw new InvalidOperationException($"Entity {typeof(T).Name} does not have a primary key.");

                var parameter = Expression.Parameter(typeof(T));
                var predicate = Expression.Equal(Expression.Property(parameter, idProperty.Name), Expression.Constant(id));
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                return await query.FirstOrDefaultAsync(lambda);
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] props)
        {
            if (props.Length > 0)
            {
                var query = _context.Set<T>().AsNoTracking().Include(props.First());
                foreach (var path in props.Skip(1))
                {
                    query = query.Include(path);
                }

                if (filter == null)
                    return await query.ToListAsync();
                return await query.Where(filter).ToListAsync();
            }

            var result = _context.Set<T>();

            if (filter == null)
                return await result.ToListAsync();
            return await result.Where(filter).ToListAsync();
        }

        public virtual async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAsync();
        }

        public virtual async Task Update(T newEntity)
        {            
            _context.Set<T>().Update(newEntity);
            await SaveAsync($"could not update entity: {newEntity} with new data: {newEntity}");
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        protected virtual async Task SaveAsync(string errorMessage = null)
        {
            if (await _context.SaveChangesAsync() == 0)
                throw new DbUpdateException(errorMessage != null ? errorMessage : "could not save changes to DB.");
        }

    }
}
