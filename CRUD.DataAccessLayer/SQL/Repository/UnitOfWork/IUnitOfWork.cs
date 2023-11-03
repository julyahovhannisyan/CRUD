namespace CRUD.DataAccessLayer.SQL.Repository.UnitOfWork;

public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
        void Rollback();
        IArticleRepository ArticleRepository();
    }
