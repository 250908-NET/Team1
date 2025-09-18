public static class DateAndTime
{
    public static void MapDateTimeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/date/today", () =>
        {
            DateTime localDate = DateTime.Now;

            var Dates = new
            {
                ShortDate = localDate.ToString("d"),
                LongDate = localDate.ToString("D"),
                ShortFullDate = localDate.ToString("f"),
                LongFullDate = localDate.ToString("F")
            };

            return Results.Json(Dates);
        });

        app.MapGet("/date/age/{birthYear:int}", (int birthYear) =>
        {
            int age = DateTime.Now.Year - birthYear;

            return Results.Json(new { birthYear, age });
        });

        app.MapGet("/date/daysbetween/{date1}/{date2}", (string date1, string date2) =>
        {
            if (DateTime.TryParse(date1, out var d1) && DateTime.TryParse(date2, out var d2))
            {
                int daysBetween = Math.Abs((d2 - d1).Days);
                return Results.Json(new { date1 = d1.ToShortDateString(), date2 = d2.ToShortDateString(), daysBetween });
            }
            else
            {
                return Results.BadRequest("Both dates must be valid and in a supported format (e.g. yyyy-MM-dd).");
            }
        });

        app.MapGet("/date/weekday/{date}", (string date) =>
        {
            if (DateTime.TryParse(date, out var parsedDate))
            {
                var dayOfWeek = parsedDate.DayOfWeek.ToString();
                return Results.Json(new { date = parsedDate.ToShortDateString(), dayOfWeek });
            }
            else
            {
                return Results.BadRequest("Invalid date format. Use yyyy-MM-dd or another valid format.");
            }
        });
    }
}