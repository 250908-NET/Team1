using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Challenge 1: Calculator
//app.MapCalculatorEndpoints();

// //Challenge 2: String Manipulation
//app.MapTextEndpoints();

// //Challenge 3: Number Games
//app.MapNumbersEndpoints();

// //Challenge 4: Date and Time Fun
//app.MapDateTimeEndpoints();

// //Challenge 5: Simple Collections
//app.MapColorsEndpoints();

// //Challenge 6: Temperature Converter
//app.MapTemperatureEndpoints();

// //Challenge 7: Password Generator
//app.MapPasswordEndpoints();

//Challenge 8: Simple Validator
//app.MapSimpleValidatorEndPoints();

// //Challenge 9: Unit Converter
//app.MapUnitConverterEndpoints();

// //Challenge 10: Weather History
//app.MapWeatherHistoryEndpoints();

// //Challenge 11: Simple Games
app.MapSimpleGamesEndpoints();

app.Run();