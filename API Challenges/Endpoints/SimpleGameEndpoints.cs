public static class SimpleGameEndpoints
{
    private static int? secretNumber = null;
    private static readonly Random random = new Random();

    public static void MapSimpleGamesEndpoints(this WebApplication app)
    {
        //Guess the Number
        app.MapGet("/game/guess/{number:int}", (int number) =>
        {
            // Initialize secret number if not already set
            if (secretNumber == null)
            {
                secretNumber = random.Next(1, 100);
            }

            // Validate input
            if (number < 1 || number > 100)
            {
                return Results.BadRequest(new { message = "Please enter a number between 1 and 100." });
            }

            // Compare guess
            if (number < secretNumber)
            {
                return Results.Ok(new { result = "Too low! Try again." });
            }
            else if (number > secretNumber)
            {
                return Results.Ok(new { result = "Too high! Try again." });
            }
            else
            {
                // Reset for next round
                secretNumber = null;
                return Results.Ok(new { result = " Congratulations! You guessed the number!" });
            }
        });

        //  Reset the game manually
        app.MapPost("/game/reset", () =>
        {
            secretNumber = random.Next(1, 100);
            return Results.Ok(new { message = "Game has been reset. A new number has been chosen between 1 and 100." });
        });
    }
}
