/*
Parking Lot:
Stephen A. | |
Kush G.
Tevin Johnson (TJ) | |
Husankhuja Nizomkhujaev | |
Daniel Scally
Ariel Soler
*/

/*
Tasks
word bank: TJ
Initialize player guess/ answer:
Get player input:


*/
using System;
using System.Diagnostics.Contracts;

namespace HangmanCA
{
    class Program
    {
        public static string answer = "";
        public static HashSet<char> guessed_letters = new HashSet<char> { };
        public static int wrong_guesses = 0;
        public static List<string> words = new List<string> { 
            "chair", 
            "table", 
            "music", 
            "light", 
            "water", 
            "happy", 
            "green", 
            "smile", 
            "phone", 
            "bread" 
        };
        public static void Main(string[] args)
        {
            initGame();
            // indicate application startup
            Console.WriteLine("Starting Hangman Game...");

            // placeholer for game setup and initlialization
            Console.WriteLine("Welcome to Hangman!");

            // continue game wh
            while (!gameEnded())
            {
                displayState();
                // get player guess
                char guess = playerInput();
                // process player guess
                processGuess(guess);
                ///track number of wrong guesses
                ///display game state
                
            }
            // indicate application shutdown
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
            Console.WriteLine("\nExiting Hangman Game...");
        }

        // ### 3. Get Player Input
        // - Ask player to enter one letter
        // - Convert to lowercase
        // - Basic validation: reject if not a single letter
        static char playerInput()
        {
            Console.Write("Enter a Letter: ");
            string input = Console.ReadLine().ToLower().Trim();

            if (!string.IsNullOrWhiteSpace(input) && char.IsLetter(input[0]) && input.Length == 1)
            {
                return input[0];
            }
            return 'a';
        }
        // Husankhuja Nizomkhujaev
        // ### 4. Process the Guess
        // - If letter is in the word → reveal all instances of that letter
        // - If letter is not in the word → add 1 to wrong guess counter
        // - Track all guessed letters to show the player
        static void processGuess(char guess) 
        {
            if (guessed_letters.Contains(guess))
            {
                Console.WriteLine("You already guessed that Letter");
                return;
            }

            guessed_letters.Add(guess);
            
            // check if answer does not contains guess
            if (!answer.Contains(guess)) {
                wrong_guesses++;
            }
        }
        
        // ### 2. Display the Game State
        // Show this information each turn:
        // ```
        // Word: c _ t
        // Guessed letters: a, e, i, c, t
        // Wrong guesses: 2 out of 6
        // ```
        static void displayState()
        {
            // Console.Clear();
            Console.WriteLine();
            Console.Write("Word:");

            for (int i = 0; i < answer.Length; i++)
            {
                if (guessed_letters.Contains(answer[i]))
                    Console.Write(" " + answer[i]);
                else
                    Console.Write(" _");
            }

            Console.Write($"\nGuessed Letters: {string.Join(", ", guessed_letters)}");
            Console.WriteLine("\nWrong guesses: " + wrong_guesses + " out of 6");

        }

        // ### 1. Word Selection
        // - Pick one word randomly when the game starts
        static void initGame() {
            Random rand = new Random();
            answer = words[rand.Next(words.Count)];
        }

        // ### 5. Check Win/Lose
        // - **Win**: All letters in the word have been revealed
        // - **Lose**: 6 wrong guesses reached
        // - Display appropriate message and end the game
        static bool gameEnded() {
            bool hasEnded = false;

            if (wrong_guesses >= 6) {
                Console.WriteLine("\nToo many guesses! You lose.");
                hasEnded = true;
            }

            if (allLettersGuessed()) {
                Console.WriteLine($"\nCongratulations! You guessed the word: {answer}");
                hasEnded = true;
            }

            return hasEnded;
        }

        static bool allLettersGuessed() {
            bool result = true;

            foreach (char letter in answer) {
                if (!guessed_letters.Contains(letter)) result = false;
            }

            return result;
        }
    }
}