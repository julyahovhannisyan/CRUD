using CRUD.BusinessLayer.Helper;
using CRUD.BusinessLayer.Models;
using CRUD.DataAccessLayer.Cache.Entities;
using CRUD.DataAccessLayer.Cache.Services;
using CRUD.DataAccessLayer.SQL.Entities;
using CRUD.DataAccessLayer.SQL.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace CRUD.BusinessLayer.Services.Implementation;

public class ArticleService : IArticleService
{
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CRUDContext _context;

    public ArticleService(ICacheService cacheService, IUnitOfWork unitOfWork, CRUDContext context)
    {
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<IEnumerable<ArticleModel>> GetArticlesAsync()
    {
        var cacheArticles = await _cacheService.GetAllByPatternAsync<ArticleCacheModel>(KeyProvider.ArticleKeyPrefix);

        var result = new List<ArticleModel>();

        foreach (var article in cacheArticles)
            result.Add(new ArticleModel
            {
                Id = Int32.Parse(article.key),
                Content = article.value.Content,
                PublishedDate = article.value.PublishedDate,
                Title = article.value.Title
            });

        return result;
    }

    public async Task<ArticleModel> GetArticleAsync(int id)
    {
        var result = await _cacheService.GetValueAsync<ArticleModel>(KeyProvider.ArticleKey(id));
        if (result == null)
        {
            var entity = await _unitOfWork.ArticleRepository().GetByIdAsync(id);

            result = new ArticleModel
            {
                Id = entity.Id,
                Content = entity.Content,
                PublishedDate = entity.PublishedDate,
                Title = entity.Title
            };

            await _cacheService.SetValueAsync(KeyProvider.ArticleKey(id), result);
        }

        return result;
    }

    public async Task UpdateArticleAsync(ArticleModel article)
    {
        _unitOfWork.ArticleRepository().Update(new Article
        {
            Id = article.Id,
            Content = article.Content,
            PublishedDate = article.PublishedDate,
            Title = article.Title
        });

        await _cacheService.SetValueAsync(KeyProvider.ArticleKey(article.Id), article);

        await _unitOfWork.CompleteAsync();
    }

    public async Task CreateArticleAsync(int id, ArticleModel article)
    {
        await _unitOfWork.ArticleRepository().CreateAsync(new Article
        {
            Id = article.Id,
            Content = article.Content,
            PublishedDate = article.PublishedDate,
            Title = article.Title
        });

        await _cacheService.SetValueAsync(KeyProvider.ArticleKey(id), article);

        await _unitOfWork.CompleteAsync();
    }

    public async Task RemoveArticleAsync(int id)
    {
        var articleForRemove = await _unitOfWork.ArticleRepository().GetByIdAsync(id);

        _unitOfWork.ArticleRepository().Remove(articleForRemove);

        await _cacheService.RemoveAsync(KeyProvider.ArticleKey(id));

        await _unitOfWork.CompleteAsync();
    }

    public async Task CreateArticlesAsync(IEnumerable<ArticleModel> articles)
    {
        foreach (var article in articles)
        {
            await _cacheService.SetValueAsync(KeyProvider.ArticleKey(article.Id), article);

            await _unitOfWork.ArticleRepository().CreateAsync(new Article 
            {
                Id=article.Id,
                Content = article.Content,
                PublishedDate = article.PublishedDate,
                Title = article.Title
            });

            await _unitOfWork.CompleteAsync();
        }
    }
}