public static class SimpleGameEndpoints
{
    private class GuessNumberRequest
    {
        public int SessionId { get; }
        public int Guess { get; }
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
        app.MapPost("/game/guess-number", async (HttpContext context) =>
        {
            GuessNumberRequest request = await context.ReadFromJsonAsync<GuessNumberRequest>();

            // if no sessionId, make one and return intro message. ignore any number
            // if sessionId:
            //   if no number, return message asking for number
            //   if number, check against secret number and return result in message

            if (request.SessionId == 0)
            {
                // Initialize a new session
                int currentSession = sessionCounter++;
                int secretNumber = random.Next(1, 100);
                sessionStorage.Add(currentSession, secretNumber);

                // return Results.Ok(request);
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

        //  Reset the game manually
        // app.MapPost("/game/reset", () =>
        // {
        //     secretNumber = random.Next(1, 100);
        //     return Results.Ok(new { message = "Game has been reset. A new number has been chosen between 1 and 100." });
        // });
    }
}
