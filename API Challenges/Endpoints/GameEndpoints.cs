public static class GameEndpoints
{
    private static int? secretNumber = null;
    private static readonly Random random = new Random();

    public static void MapGamesEndpoints(this WebApplication app)
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

//dice roll game
app.MapGet("/game/dice/{sides:int}/{count:int}", (int sides, int count) =>
        {
            if (sides < 2 || count < 1)
                return Results.BadRequest(new { message = "Sides must be >=2 and count >=1." });


// to store the number - rolled(created a list for holding that value)
//initially it will be empty

            var rolls = new List<int>();

            for (int i = 0; i < count; i++)
            // random.Next - will hold min and max value
            
                rolls.Add(random.Next(1, sides + 1));

                //returning -with sides,count and rolls

            return Results.Ok(new { sides, count, rolls });
        });