using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder =WebApplication.CreateBuilder(args);

var app = builder.Build();

//for getting a reverse string

app.MapGet("/text/reverse/{text}",(string text) =>
{
    char[] charArray =text.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
});

// to get the uppercase

app.MapGet("/text/uppercase/{text}",(string text)=>
{
    return text.ToUpper();
});

//to lowercase

app.MapGet("/text/lowercase/{text}",(string text)=>
{
    return text.ToLower();
});

//count characters , word and vowels

app.MapGet("/text/count/{text}",(string text)=>{

    int charCount= text.Length;
    int wordCount=text.Split(' ',StringSplitOptions.RemoveEmptyEntries).Length;
    int vowelCount= text.Count(c=>"aeiouAEIOU".Contains(c));
    return Results.Json(new
    {
        characters= charCount,
        words=wordCount,
        vowels=vowelCount
    });
});

//heck if palidrome

app.MapGet("text/palindrome/{text}",(string text)=>
{
    string cleanedText = new string(text.Where(char.IsLetterOrDigit).ToArray()).ToLower();
    string reversedText = new string(cleanedText.Reverse().ToArray());
    bool isPalindrome = cleanedText == reversedText;
    return Results.Json(new
    {
        text=text,
        isPalindrome=isPalindrome
    });
});

app.Run();

