public static class CalculatorEndpoints
{
    public static void MapCalculatorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/calculator/add/{a}/{b}", (int a, int b) =>
        {
            return new
            {
                operation = "add",
                result = a + b
            };
        });
    }
}