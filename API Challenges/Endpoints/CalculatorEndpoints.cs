public static class CalculatorEndpoints
{
    public static void MapCalculatorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/calculator/add/{a}/{b}", (HttpContext context, int a, int b) =>
        {
            return Results.Ok(new
            {
                operation = "add",
                result = a + b
            });
        });

        app.MapGet("/calculator/subtract/{a}/{b}", (HttpContext context, int a, int b) =>
        {
            return Results.Ok(new
            {
                operation = "subtract",
                result = a - b
            });
        });

        app.MapGet("/calculator/multiply/{a}/{b}", (HttpContext context, int a, int b) =>
        {
            return Results.Ok(new
            {
                operation = "multiply",
                result = a * b
            });
        });

        app.MapGet("/calculator/divide/{a}/{b}", (HttpContext context, int a, int b) =>
        {
            if (b == 0)
            {
                return Results.BadRequest(new { error = "Division by zero is not allowed." });
            }
            return Results.Ok(new
            {
                operation = "divide",
                result = a / b
            });
        });
    }
}