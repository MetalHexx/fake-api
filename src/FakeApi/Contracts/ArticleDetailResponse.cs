using FakeApi.Domain;
namespace FakeApi.Contracts;
public class ArticleDetailResponse
{
    public required Article Article { get; init; }
    public required IReadOnlyList<ArticleSummary> Related { get; init; }
}
