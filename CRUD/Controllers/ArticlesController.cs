using CRUD.BusinessLayer.Models;
using CRUD.BusinessLayer.Services;
using CRUD.Models;

using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly CRUDContext _context;

    public ArticlesController(IArticleService articleService, CRUDContext context)
    {
        _articleService = articleService;
        _context = context; 
    }

    [HttpGet("articles")]
    public async Task<ActionResult<IEnumerable<GetArticleDto>>> GetArticles()
    {
        var articles = await _articleService.GetArticlesAsync();
        return Ok(articles);
    }

    [HttpGet("article/{id}")]
    public async Task<ActionResult<GetArticleDto>> GetArticle(int id)
    {
        var article = await _articleService.GetArticleAsync(id);

        var resultDto = new GetArticleDto 
        {
            Id = article.Id,
            Content = article.Content,
            Title = article.Title,
        };

        return Ok(resultDto);
    }

    [HttpPost("article")]
    public async Task<ActionResult<PutArticleDto>> CreateArticle([FromBody] CreateArticleDto article)
    {
        if (article == null)
        {
            return BadRequest("Invalid payload");
        }

        var articleModel = new ArticleModel
        {
            Title = article.Title,
            Content = article.Content,
            PublishedDate = article.PublishedDate,
        };

        await _articleService.UpdateArticleAsync(articleModel);
        return Ok();
        
    }

    [HttpPut("article/{id}")]
    public async Task<IActionResult> UpdateArticle(int id, [FromBody] UpdateArticleDto article)
    {
        var articleModel = new ArticleModel
        {
            Id = id,
            Title = article.Title,
            Content = article.Content,
            PublishedDate = article.PublishedDate,
        };
        await _articleService.UpdateArticleAsync(articleModel);
        return NoContent();
    }

    [HttpDelete("article/{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        await _articleService.RemoveArticleAsync(id);
        return NoContent();
    }
}
