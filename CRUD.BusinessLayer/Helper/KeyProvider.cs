namespace CRUD.BusinessLayer.Helper;

public static class KeyProvider
{
    public const string ArticleKeyPrefix = "Article_";

    public static string ArticleKey(int articleId) => $"{ArticleKeyPrefix}_{articleId}";
}
