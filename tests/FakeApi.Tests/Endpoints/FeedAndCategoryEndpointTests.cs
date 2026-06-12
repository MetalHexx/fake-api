using System.Net;
using System.Net.Http.Json;
using FakeApi.Contracts;
using FakeApi.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class FeedAndCategoryEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public FeedAndCategoryEndpointTests(WebApplicationFactory<Program> f) => _client = f.CreateClient();

    [Fact]
    public async Task Feed_ReturnsHeroAndArticles_HeroExcludedFromList_NewestFirst()
    {
        var feed = await _client.GetFromJsonAsync<FeedResponse>("/api/feed");
        Assert.NotNull(feed);
        Assert.NotNull(feed!.Hero);
        Assert.DoesNotContain(feed.Articles, a => a.Id == feed.Hero.Id);
        for (var i = 1; i < feed.Articles.Count; i++)
            Assert.True(feed.Articles[i - 1].PublishedAt >= feed.Articles[i].PublishedAt);
        Assert.True(feed.Hero.PublishedAt >= feed.Articles[0].PublishedAt);
    }

    [Fact]
    public async Task Categories_ReturnsSixCanonicalNames()
    {
        var names = await _client.GetFromJsonAsync<List<string>>("/api/categories");
        Assert.Equal(Category.All, names);
    }
}
