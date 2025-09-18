using Microsoft.VisualBasic;

public static class ColorsEndpoints
{
    public static void MapColorsEndpoints(this IEndpointRouteBuilder app)
    {
        var favoriteColors = new List<string> { "Red", "Yellow", "Blue", "Green", "Purple", "Orange" };

        app.MapGet("/colors", () =>
        {
            return Results.Json(favoriteColors);
        });

        app.MapGet("/colors/random", () =>
        {
            Random rnd = new Random();
            string randomColor = favoriteColors[rnd.Next(favoriteColors.Count)];
            return Results.Json(randomColor);
        });

        app.MapGet("/colors/search/{letter}", (char letter) =>
        {
            var matches = favoriteColors.Where(color => color.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();

            return Results.Json(matches);
        });

        app.MapPost("/colors/add/{color}", (string color) =>
        {
            if (string.IsNullOrWhiteSpace(color))
                return Results.BadRequest("No color designated.");

            if (favoriteColors.Contains(color, StringComparer.OrdinalIgnoreCase))
                return Results.Conflict($"{color} already exists.");

            favoriteColors.Add(color);
            return Results.Ok(new { message = $"{color} added!", total = favoriteColors.Count });
        });
    }
}