using Microsoft.AspNetCore.Http.HttpResults;
using System.Globalization;

public static class WeatherHistoryEndpoints
{
    public static Dictionary<DateTime, int> WeatherData = new Dictionary<DateTime, int>();

    public static void MapWeatherHistoryEndpoints(this WebApplication app)
    {
        app.MapGet("/weatherhistory", () =>
        {
            return WeatherData.ToList();
        });

        app.MapGet("/weatherhistory/{date:datetime}", (DateTime date) =>
        {
            if (WeatherData.TryGetValue(date.Date, out int temperature))
            {
                return Results.Ok(new KeyValuePair<DateTime, int>(date.Date, temperature));
            }
            return Results.NotFound("Date not found");
        });

        app.MapPost("/weatherhistory", (DateTime date, int temperature) =>
        {
            if (!WeatherData.TryAdd(date.Date, temperature))
            {
                return Results.Conflict("Date already exists");
            }

            return Results.Created($"/weatherhistory/{date.Date.ToString("yyyy-MM-dd")}", new KeyValuePair<DateTime, int>(date.Date, temperature));
        });

        app.MapDelete("/weatherhistory/{date:datetime}", (DateTime date) =>
        {
            if (WeatherData.Remove(date.Date))
            {
                return Results.Ok();
            }

            return Results.NotFound("Date not found");
        });
    }
}