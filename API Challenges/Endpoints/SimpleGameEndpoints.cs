public static class SimpleGameEndpoints
{
    private class GuessNumberRequest
    {
        public int SessionId { get; set; }
        public int Guess { get; set; }
    }
    private class GuessNumberResponse
    {
        public int SessionId { get; set; }
        public required string Message { get; set; }
    }

    private static Dictionary<int, int> sessionStorage = new Dictionary<int, int>();
    private static int sessionCounter = 1;

    private static readonly Random random = new Random();

    public static void MapSimpleGamesEndpoints(this WebApplication app)
    {

        app.MapPost("/game/guess-number", (GuessNumberRequest request) =>
        {

            if (request.SessionId == 0)
            {
                // Initialize a new session
                int currentSession = sessionCounter++;
                int secretNumber = random.Next(1, 100);
                sessionStorage.Add(currentSession, secretNumber);

                // Return new sessionId and intro message
                return Results.Ok(new GuessNumberResponse
                {
                    SessionId = currentSession,
                    Message = "Welcome to the number guessing game! Guess a number between 1 and 100. (Format your guess in a JSON with the provided `SessionId` and with `Guess` equal to your guess.)"
                });
            }
            else
            {
                int secretNumber = sessionStorage[request.SessionId];
                int guess = request.Guess;

                // Validate input
                if (guess == null || guess < 1 || guess > 100)
                {
                    return Results.Ok(new GuessNumberResponse
                    {
                        SessionId = request.SessionId,
                        Message = "Please guess a number between 1 and 100. (Format your guess in a JSON with the provided `SessionId` and with `Guess` equal to your guess.)"
                    });
                }

                // Compare guess
                if (guess < secretNumber)
                {
                    return Results.Ok(new GuessNumberResponse
                    {
                        SessionId = request.SessionId,
                        Message = "Too low! Try again."
                    });
                }
                else if (guess > secretNumber)
                {
                    return Results.Ok(new GuessNumberResponse
                    {
                        SessionId = request.SessionId,
                        Message = "Too high! Try again."
                    });
                }
                else
                {
                    // Reset for next round
                    sessionStorage[request.SessionId] = random.Next(1, 100);

                    return Results.Ok(new GuessNumberResponse
                    {
                        SessionId = request.SessionId,
                        Message = $"Congratulations! You guessed the number: {secretNumber}. The game has been reset--guess a new number!"
                    });
                }
            }
        });

        // Flip Coin
        app.MapGet("/game/coin-flip/{count}", (int count) =>
        {
            List<string> results = [];
            for (int i = 0; i < count; i++)
            {
                results.Add(random.Next(2) == 0 ? "Heads" : "Tails");
            }
            return Results.Ok(results);
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
