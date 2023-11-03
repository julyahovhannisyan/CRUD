using System.Linq.Expressions;

namespace CRUD.DataAccessLayer.SQL.Repository;

public interface IBaseCRUDRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByIdAsync(int id);
    Task CreateAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}

