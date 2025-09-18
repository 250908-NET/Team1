using System.Text;

public static class NumberGamesEndpoints
{
    public static void MapNumbersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/numbers/fizzbuzz/{count}", (int count) =>
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= count; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    sb.AppendLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    sb.AppendLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    sb.AppendLine("Buzz");
                }
            }

            return sb.ToString();
        });

        app.MapGet("/numbers/prime/{number}", (int number) =>
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        });

        app.MapGet("/numbers/fibonacci/{number}", (int number) =>
        {
            if (number < 0) return Results.BadRequest("Number must be non-negative.");

            if (number == 0) return Results.Ok(0);
            if (number == 1) return Results.Ok(1);

            int a = 0, b = 1, c = 0;
            for (int i = 2; i <= number; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            return Results.Ok(b);
        });
    }
}
