using FakeApi.Data;
using FakeApi.Domain;

namespace FakeApi.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("/api/categories", (IArticleStore store) =>
        {
            return Results.Ok(store.GetCategories());
        });

        app.MapGet("/api/categories/{category}/articles", (string category, IArticleStore store) =>
        {
            if (!Category.TryNormalize(category, out var canonical))
                return Results.NotFound();

            var articles = store.GetByCategoryNewestFirst(canonical)
                .Select(a => a.ToSummary())
                .ToList();

            return Results.Ok(articles);
        });
    }
}
