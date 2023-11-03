using CRUD.BusinessLayer.Models;

namespace CRUD.BusinessLayer.Services;

public interface IArticleService
{
    Task<IEnumerable<ArticleModel>> GetArticlesAsync();
    Task<ArticleModel> GetArticleAsync(int id);
    Task CreateArticleAsync(int id, ArticleModel article);
    Task CreateArticlesAsync(IEnumerable<ArticleModel> articles);
    Task UpdateArticleAsync(ArticleModel article);
    Task RemoveArticleAsync(int id);
}
