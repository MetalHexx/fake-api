namespace FakeApi.Domain;

public static class Category
{
    public static readonly IReadOnlyList<string> All = new[]
    {
        "World", "Technology", "Business", "Sports", "Entertainment", "Science"
    };

    public static bool TryNormalize(string? input, out string canonical)
    {
        canonical = string.Empty;
        if (string.IsNullOrWhiteSpace(input)) return false;
        foreach (var name in All)
        {
            if (string.Equals(name, input, StringComparison.OrdinalIgnoreCase))
            {
                canonical = name;
                return true;
            }
        }
        return false;
    }
}
