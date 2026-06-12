namespace FakeApi.Domain;

public class Article
{
    public required string Id { get; init; }
    public required string Headline { get; init; }
    public required string Dek { get; init; }
    public required string Category { get; init; }
    public required string Author { get; init; }
    public required string AuthorRole { get; init; }
    public required DateTimeOffset PublishedAt { get; init; }
    public required int ReadMinutes { get; init; }
    public required IReadOnlyList<string> BodyParagraphs { get; init; }
    public required string PullQuote { get; init; }
    public required string PullQuoteAttribution { get; init; }

    public ArticleSummary ToSummary() => new()
    {
        Id = Id, Headline = Headline, Dek = Dek, Category = Category,
        Author = Author, AuthorRole = AuthorRole,
        PublishedAt = PublishedAt, ReadMinutes = ReadMinutes
    };
}
