using FakeApi.Data;
using FakeApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IArticleStore, InMemoryArticleStore>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapFeedEndpoints();
app.MapCategoryEndpoints();

app.Run();

public partial class Program { }
