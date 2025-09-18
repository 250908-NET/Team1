# C# Challenges for Minimal API Practice

## Challenge Progression

✅ **Challenges 1-3**: Basic C# syntax, operations, and control flow <br>
✅ **Challenges 4-6**: Working with built-in types and classes <br>
✅ **Challenges 7-9**: String manipulation and data validation <br>
✅ **Challenge 10/11**: Combining concepts and adding simple state <br>

## Challenge 1: Basic Calculator
**Goal**: Practice basic operations and parameter handling <br>
✅ Create endpoint `/calculator/add/{a}/{b}` that returns sum of two numbers <br>
✅ Add endpoints for subtract, multiply, and divide <br>
✅ Handle division by zero with proper error messages <br>
✅ Return results as JSON: `{"operation": "add", "result": 15}`

## Challenge 2: String Manipulator
**Goal**: Work with string methods and transformations <br>
✅ Create `/text/reverse/{text}` - returns reversed string <br>
✅ Add `/text/uppercase/{text}` and `/text/lowercase/{text}` <br>
✅ Create `/text/count/{text}` - returns character count, word count, vowel count <br>
✅ Add `/text/palindrome/{text}` - checks if text is a palindrome

## Challenge 3: Number Games
**Goal**: Practice loops, conditionals, and number operations <br>
✅ Create `/numbers/fizzbuzz/{count}` - returns FizzBuzz sequence up to count <br>
✅ Add `/numbers/prime/{number}` - checks if number is prime <br>
✅ Create `/numbers/fibonacci/{count}` - returns first N Fibonacci numbers <br>
✅ Add `/numbers/factors/{number}` - returns all factors of a number

## Challenge 4: Date and Time Fun
**Goal**: Work with DateTime and formatting <br>
✅ Create `/date/today` - returns current date in different formats <br>
✅ Add `/date/age/{birthYear}` - calculates age from birth year <br>
✅ Create `/date/daysbetween/{date1}/{date2}` - calculates days between dates <br>
✅ Add `/date/weekday/{date}` - returns day of week for given date

## Challenge 5: Simple Collections
**Goal**: Practice working with lists and basic LINQ <br>
✅ Create `/colors` endpoint that returns a predefined list of favorite colors <br>
✅ Add `/colors/random` - returns a random color from the list <br>
✅ Create `/colors/search/{letter}` - returns colors starting with that letter <br>
✅ Add `/colors/add/{color}` (POST) - adds new color to the list

## Challenge 6: Temperature Converter
**Goal**: Practice calculations and different data formats <br>
✅ Create `/temp/celsius-to-fahrenheit/{temp}`  <br>
✅ Add `/temp/fahrenheit-to-celsius/{temp}` <br>
✅ Create `/temp/kelvin-to-celsius/{temp}` and reverse <br>
✅ Add `/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}` - compares temperatures

## Challenge 7: Password Generator
**Goal**: Work with random generation and string building <br>
✅ Create `/password/simple/{length}` - generates random letters/numbers <br>
✅ Add `/password/complex/{length}` - includes special characters <br>
✅ Create `/password/memorable/{words}` - generates passphrase with N words <br>
✅ Add `/password/strength/{password}` - rates password strength

## Challenge 8: Simple Validator
**Goal**: Practice validation logic and boolean operations <br>
✅ Create `/validate/email/{email}` - basic email format validation <br>
✅ Add `/validate/phone/{phone}` - validates phone number format <br>
✅ Create `/validate/creditcard/{number}` - Luhn algorithm validation <br>
✅ Add `/validate/strongpassword/{password}` - checks password rules

## Challenge 9: Unit Converter
**Goal**: Work with different measurement systems <br>
✅ Create `/convert/length/{value}/{fromUnit}/{toUnit}` (meters, feet, inches) <br>
✅ Add `/convert/weight/{value}/{fromUnit}/{toUnit}` (kg, lbs, ounces) <br>
✅ Create `/convert/volume/{value}/{fromUnit}/{toUnit}` (liters, gallons, cups) <br>
✅ Add `/convert/list-units/{type}` - returns available units for each type

## Challenge 10: Weather History
**Goal**: Add persistence and CRUD operations <br>
✅ Create a simple in-memory list to store weather forecasts <br>
✅ Add POST endpoint to save a weather forecast <br>
✅ Modify GET to return saved forecasts instead of random ones <br>
✅ Add DELETE endpoint to remove forecasts by date

## Challenge 11: Simple Games
**Goal**: Combine multiple concepts in mini-games <br>
✅ Create `/game/guess-number` (POST) - number guessing game with session <br>
✅ Add `/game/rock-paper-scissors/{choice}` (GET) - play against computer <br>
✅ Create `/game/dice/{sides}/{count}` (GET) - roll N dice with X sides <br>
✅ Add `/game/coin-flip/{count}` (GET) - flip coins and return results

## Sample Implementation Pattern

Each challenge should follow this basic structure:

```csharp
// Example for Challenge 1
app.MapGet("/calculator/add/{a}/{b}", (double a, double b) => 
{
    var result = a + b;
    return new { operation = "add", input1 = a, input2 = b, result = result };
});

app.MapGet("/calculator/divide/{a}/{b}", (double a, double b) => 
{
    if (b == 0)
        return Results.BadRequest(new { error = "Cannot divide by zero" });
    
    var result = a / b;
    return Results.Ok(new { operation = "divide", input1 = a, input2 = b, result = result });
});
```