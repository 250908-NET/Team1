public static class TemperatureEndpoints
{
    public static void MapTemperatureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/temp/celsius-to-fahrenheit/{temp}", (float temp) =>
        {
            float converted = temp * 9 / 5 + 32;
            return new
            {
                unit = "fahrenheit",
                result = converted
            };
        });
        // fahrenheit to celsius
        app.MapGet("/temp/fahrenheit-to-celsius/{temp}", (float temp) =>
        {
            float converted = (temp - 32) * 5 / 9;
            return new
            {
                unit = "celsius",
                result = converted
            };
        });
        // kelvin to celsius
        app.MapGet("/temp/kelvin-to-celsius/{temp}", (float temp) =>
        {
            float converted = temp - 273.15f;
            return new
            {
                unit = "celsius",
                result = converted
            };
        });
        // celsius to kelvin
        app.MapGet("/temp/celsius-to-kelvin/{temp}", (float temp) =>
        {
            float converted = temp + 273.15f;
            return new
            {
                unit = "kelvin",
                result = converted
            };
        });
        // compare two temperatures
        app.MapGet("/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}", (float temp1, string unit1, float temp2, string unit2) =>
        {
            string result;

            try
            {
                float t1InCelsius = ConvertToCelsius(temp1, unit1);
                float t2InCelsius = ConvertToCelsius(temp2, unit2);

                string comparisonMessage = "equal";
                if (t1InCelsius < t2InCelsius)
                {
                    comparisonMessage = "temp1 is less than temp2";
                }
                else if (t1InCelsius > t2InCelsius)
                {
                    comparisonMessage = "temp1 is greater than temp2";
                }

                result = comparisonMessage;
            }
            catch (Exception ex)
            {
                result = "Invalid input: " + ex.Message;
            }

            return new { result = result };
        });

        static float ConvertToCelsius(float temp, string unit)
        {
            switch (unit.ToLower())
            {
                case "celsius":
                    return temp;
                case "fahrenheit":
                    return (temp - 32) * 5 / 9;
                case "kelvin":
                    return temp - 273.15f;
                default:
                    throw new ArgumentException($"Invalid unit '{unit}'. Use 'celsius', 'fahrenheit', or 'kelvin'.");
            }
        }
    }
}