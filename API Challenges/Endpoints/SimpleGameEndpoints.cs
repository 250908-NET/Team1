public static class SimpleGameEndpoints
{
    private static int? secretNumber = null;
    private static readonly Random random = new Random();

    public static void MapSimpleGamesEndpoints(this WebApplication app)
    {
        // Guess the Number
        app.MapGet("/game/guess/{number:int}", (int number) =>
        {
            if (secretNumber == null)
                secretNumber = random.Next(1, 100);

            if (number < 1 || number > 100)
                return Results.BadRequest(new { message = "Please enter a number between 1 and 100." });

            if (number < secretNumber)
                return Results.Ok(new { result = "Too low! Try again." });
            else if (number > secretNumber)
                return Results.Ok(new { result = "Too high! Try again." });
            else
            {
                secretNumber = null;
                return Results.Ok(new { result = "Congratulations! You guessed the number!" });
            }
        });

        // Reset the game manually
        app.MapPost("/game/reset", () =>
        {
            secretNumber = random.Next(1, 100);
            return Results.Ok(new { message = "Game has been reset. A new number has been chosen between 1 and 100." });
        });

        app.MapGet("/game/rock-paper-scissors/{choice}", (string choice) =>
        {
            var userChoice = choice.ToLower();
            var gameChoices = new[] { "rock", "paper", "scissors" };
            string result;

            if (gameChoices.Contains(userChoice))
            {
                string cpuChoice = gameChoices[random.Next(gameChoices.Length)];

                if (cpuChoice.Equals(choice))
                    result = "It was a Draw! D:";
                else if (userChoice == "rock" && cpuChoice == "scissors"
                            || userChoice == "paper" && cpuChoice == "rock"
                            || userChoice == "scissors" && cpuChoice == "paper")
                    result = "You Win! ^.^";
                else
                    result = "You Lost! T.T";

                string message = $"""
                Rock, Paper, Scissors, Shoot!
                {userChoice} vs {cpuChoice}
                {result}
                """;

                return Results.Text(message);
            }
            else
            {
                return Results.BadRequest("Invalid option. Valid options: Rock, Paper, or Scissors.");
            }

        });
    


        // Dice roll game
        app.MapGet("/game/dice/{sides:int}/{count:int}", (int sides, int count) =>
        {
            if (sides < 2 || count < 1)
                return Results.BadRequest(new { message = "Sides must be >=2 and count >=1." });

            var rolls = new List<int>();
            for (int i = 0; i < count; i++)
                rolls.Add(random.Next(1, sides + 1));

            return Results.Ok(new { sides, count, rolls });
        });
    }
}
