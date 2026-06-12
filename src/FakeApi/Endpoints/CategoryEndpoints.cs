using FakeApi.Data;

namespace FakeApi.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("/api/categories", (IArticleStore store) =>
        {
            return Results.Ok(store.GetCategories());
        });
    }
}
