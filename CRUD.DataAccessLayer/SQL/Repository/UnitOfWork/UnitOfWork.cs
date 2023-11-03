using Microsoft.EntityFrameworkCore;

namespace CRUD.DataAccessLayer.SQL.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly CRUDContext _dbContext;
        private bool _disposed;
        public UnitOfWork(CRUDContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        private IArticleRepository _articleRepository;
        public IArticleRepository ArticleRepository()
        {
            _articleRepository ??= new ArticleRepository(_dbContext);

            return _articleRepository;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
