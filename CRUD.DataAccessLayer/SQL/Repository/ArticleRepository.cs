using CRUD.DataAccessLayer.SQL.Entities;

namespace CRUD.DataAccessLayer.SQL.Repository;

public class ArticleRepository : BaseCRUDRepository<Article>, IArticleRepository
{
    public ArticleRepository(CRUDContext context) : base(context)
    { }
}
