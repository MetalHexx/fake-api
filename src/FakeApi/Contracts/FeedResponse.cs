using FakeApi.Domain;
namespace FakeApi.Contracts;
public class FeedResponse
{
    public required ArticleSummary Hero { get; init; }
    public required IReadOnlyList<ArticleSummary> Articles { get; init; }
}
