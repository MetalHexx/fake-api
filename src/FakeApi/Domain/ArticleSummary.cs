namespace FakeApi.Domain;

public class ArticleSummary
{
    public required string Id { get; init; }
    public required string Headline { get; init; }
    public required string Dek { get; init; }
    public required string Category { get; init; }
    public required string Author { get; init; }
    public required string AuthorRole { get; init; }
    public required DateTimeOffset PublishedAt { get; init; }
    public required int ReadMinutes { get; init; }
}
