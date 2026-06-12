using FakeApi.Data;
using FakeApi.Domain;
using Xunit;

public class ArticleSeederTests
{
    [Fact]
    public void Seed_Produces36Articles_SixPerCategory()
    {
        var articles = ArticleSeeder.Seed();
        Assert.Equal(36, articles.Count);
        foreach (var cat in Category.All)
            Assert.Equal(6, articles.Count(a => a.Category == cat));
    }

    [Fact]
    public void Seed_IsDeterministic_AcrossTwoRuns()
    {
        var first = ArticleSeeder.Seed();
        var second = ArticleSeeder.Seed();
        Assert.Equal(
            first.Select(a => (a.Id, a.Headline, a.PublishedAt)),
            second.Select(a => (a.Id, a.Headline, a.PublishedAt)));
    }

    [Fact]
    public void Seed_AssignsStableIdsAndNonEmptyContent()
    {
        var articles = ArticleSeeder.Seed();
        Assert.All(articles, a =>
        {
            Assert.False(string.IsNullOrWhiteSpace(a.Id));
            Assert.False(string.IsNullOrWhiteSpace(a.Headline));
            Assert.True(a.BodyParagraphs.Count >= 3);
            Assert.False(string.IsNullOrWhiteSpace(a.PullQuote));
        });
        Assert.Equal(articles.Select(a => a.Id).Distinct().Count(), articles.Count);
    }

    [Fact]
    public void Store_ReturnsSameInstanceContent_AndIsNewestFirstOverall()
    {
        var store = new InMemoryArticleStore();
        var all = store.GetAllNewestFirst();
        for (var i = 1; i < all.Count; i++)
            Assert.True(all[i - 1].PublishedAt >= all[i].PublishedAt);
    }
}
