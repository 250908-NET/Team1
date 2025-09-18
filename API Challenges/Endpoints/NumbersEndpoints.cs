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
    }
}
