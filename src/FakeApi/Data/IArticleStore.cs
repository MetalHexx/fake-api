using FakeApi.Domain;

namespace FakeApi.Data;

public interface IArticleStore
{
    IReadOnlyList<Article> GetAllNewestFirst();
    Article? GetById(string id);
    IReadOnlyList<Article> GetByCategoryNewestFirst(string canonicalCategory);
    IReadOnlyList<Article> GetRelated(string id, int max);
    IReadOnlyList<string> GetCategories();
}
