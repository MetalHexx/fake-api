using System.Net;
using System.Net.Http.Json;
using FakeApi.Contracts;
using FakeApi.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class ArticleAndCategoryFilterEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public ArticleAndCategoryFilterEndpointTests(WebApplicationFactory<Program> f) => _client = f.CreateClient();

    [Fact]
    public async Task Article_ReturnsFullDetail_WithUpToThreeSameCategoryRelated_ExcludingSelf()
    {
        var detail = await _client.GetFromJsonAsync<ArticleDetailResponse>("/api/articles/world-000");
        Assert.NotNull(detail);
        Assert.Equal("world-000", detail!.Article.Id);
        Assert.NotEmpty(detail.Article.BodyParagraphs);
        Assert.False(string.IsNullOrWhiteSpace(detail.Article.PullQuote));
        Assert.True(detail.Related.Count <= 3);
        Assert.All(detail.Related, r => Assert.Equal("World", r.Category));
        Assert.DoesNotContain(detail.Related, r => r.Id == "world-000");
    }

    [Fact]
    public async Task Article_UnknownId_Returns404()
    {
        var resp = await _client.GetAsync("/api/articles/does-not-exist");
        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }

    [Fact]
    public async Task CategoryArticles_CaseInsensitive_NewestFirst()
    {
        var resp = await _client.GetFromJsonAsync<List<ArticleSummary>>("/api/categories/TECHNOLOGY/articles");
        Assert.NotNull(resp);
        Assert.NotEmpty(resp!);
        Assert.All(resp, a => Assert.Equal("Technology", a.Category));
        for (var i = 1; i < resp.Count; i++)
            Assert.True(resp[i - 1].PublishedAt >= resp[i].PublishedAt);
    }

    [Fact]
    public async Task CategoryArticles_UnknownCategory_Returns404()
    {
        var resp = await _client.GetAsync("/api/categories/politics/articles");
        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }
}
