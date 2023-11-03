namespace CRUD.Models;

public class ArticleDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedDate { get; set; }
}
public class GetArticleDto : ArticleDto
{
    public int Id { get; set; }
}

public class CreateArticleDto : ArticleDto
{
}

public class UpdateArticleDto : ArticleDto
{
    public int Id { get; set; }
}
