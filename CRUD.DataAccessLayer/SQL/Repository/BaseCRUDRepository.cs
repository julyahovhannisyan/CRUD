using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DataAccessLayer.SQL.Repository
{
    public class BaseCRUDRepository<TEntity> : IBaseCRUDRepository<TEntity> where TEntity : class
    {
        private readonly CRUDContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseCRUDRepository(CRUDContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
           _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
