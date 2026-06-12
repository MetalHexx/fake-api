using FakeApi.Domain;

namespace FakeApi.Data;

public class InMemoryArticleStore : IArticleStore
{
    private readonly IReadOnlyList<Article> _allNewestFirst;
    private readonly Dictionary<string, Article> _byId;

    public InMemoryArticleStore()
    {
        var articles = ArticleSeeder.Seed();
        _allNewestFirst = articles
            .OrderByDescending(a => a.PublishedAt)
            .ToList()
            .AsReadOnly();
        _byId = _allNewestFirst.ToDictionary(a => a.Id, StringComparer.OrdinalIgnoreCase);
    }

    public IReadOnlyList<Article> GetAllNewestFirst() => _allNewestFirst;

    public Article? GetById(string id) =>
        _byId.TryGetValue(id, out var article) ? article : null;

    public IReadOnlyList<Article> GetByCategoryNewestFirst(string canonicalCategory) =>
        _allNewestFirst
            .Where(a => string.Equals(a.Category, canonicalCategory, StringComparison.OrdinalIgnoreCase))
            .ToList()
            .AsReadOnly();

    public IReadOnlyList<Article> GetRelated(string id, int max)
    {
        if (!_byId.TryGetValue(id, out var article))
            return Array.Empty<Article>();

        return _allNewestFirst
            .Where(a => !string.Equals(a.Id, id, StringComparison.OrdinalIgnoreCase) && string.Equals(a.Category, article.Category, StringComparison.OrdinalIgnoreCase))
            .Take(max)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<string> GetCategories() => Category.All;
}
