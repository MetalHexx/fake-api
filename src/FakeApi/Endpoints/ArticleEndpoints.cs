using FakeApi.Contracts;
using FakeApi.Data;

namespace FakeApi.Endpoints;

public static class ArticleEndpoints
{
    public static void MapArticleEndpoints(this WebApplication app)
    {
        app.MapGet("/api/articles/{id}", (string id, IArticleStore store) =>
        {
            var article = store.GetById(id);
            if (article is null)
                return Results.NotFound();

            var related = store.GetRelated(id, 3)
                .Select(a => a.ToSummary())
                .ToList()
                .AsReadOnly();

            return Results.Ok(new ArticleDetailResponse { Article = article, Related = related });
        });
    }
}
