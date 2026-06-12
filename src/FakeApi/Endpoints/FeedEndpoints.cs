using FakeApi.Contracts;
using FakeApi.Data;

namespace FakeApi.Endpoints;

public static class FeedEndpoints
{
    public static void MapFeedEndpoints(this WebApplication app)
    {
        app.MapGet("/api/feed", (IArticleStore store) =>
        {
            var all = store.GetAllNewestFirst();
            if (all.Count == 0)
                return Results.NotFound();

            var hero = all[0].ToSummary();
            var articles = all.Skip(1).Select(a => a.ToSummary()).ToList();

            return Results.Ok(new FeedResponse { Hero = hero, Articles = articles });
        });
    }
}
