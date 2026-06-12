using FakeApi.Domain;
using Xunit;

public class CategoryTests
{
    [Fact]
    public void All_ReturnsExactlySixCanonicalCategories()
    {
        Assert.Equal(
            new[] { "World", "Technology", "Business", "Sports", "Entertainment", "Science" },
            Category.All);
    }

    [Theory]
    [InlineData("technology", "Technology")]
    [InlineData("WORLD", "World")]
    [InlineData("Science", "Science")]
    public void TryNormalize_MatchesCanonicalNameCaseInsensitively(string input, string expected)
    {
        Assert.True(Category.TryNormalize(input, out var canonical));
        Assert.Equal(expected, canonical);
    }

    [Fact]
    public void TryNormalize_ReturnsFalseForUnknownCategory()
    {
        Assert.False(Category.TryNormalize("Politics", out _));
    }

    [Fact]
    public void Article_ExposesBodyAndPullQuote_NotPresentOnSummary()
    {
        var article = new Article
        {
            Id = "a-001", Headline = "H", Dek = "D", Category = "World",
            Author = "A", AuthorRole = "Staff Writer", PublishedAt = System.DateTimeOffset.UtcNow,
            ReadMinutes = 5, BodyParagraphs = new[] { "p1", "p2" },
            PullQuote = "q", PullQuoteAttribution = "attr"
        };
        Assert.Equal(2, article.BodyParagraphs.Count);
        Assert.Equal("q", article.PullQuote);
    }
}
