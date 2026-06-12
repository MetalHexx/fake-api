using FakeApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IArticleStore, InMemoryArticleStore>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public partial class Program { }
