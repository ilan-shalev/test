using System.Linq.Expressions;

namespace Test.Data.Repositories;

public interface IRepository<T>
{
    Task<T?> Get<TKey>(TKey id, bool eager = false, params Expression<Func<T, object>>[] props);

    Task<IEnumerable<T>> GetAll(Expression<Func<T,
        bool>>? filter = null, params Expression<Func<T, object>>[] props);

    Task Add(T entity);

    Task Update(T newEntity);

    Task Remove(T entity);
}
