public static class UnitConverterEndpoints
{
    private static Dictionary<string, Dictionary<string, double>> lengthConversionFactors =
        new Dictionary<string, Dictionary<string, double>>()
        {
            { "meters", new Dictionary<string, double> {
                { "meters", 1.0 },
                { "feet", 3.28084 },
                { "inches", 39.3701 }
            } },
            { "feet", new Dictionary<string, double> {
                { "meters", 0.3048 },
                { "feet", 1.0 },
                { "inches", 12 }
            } },
            { "inches", new Dictionary<string, double> {
                { "meters", 0.0254 },
                { "feet", 0.0833333 },
                { "inches", 1.0 }
            } }
        };

    private static Dictionary<string, Dictionary<string, double>> weightConversionFactors =
        new Dictionary<string, Dictionary<string, double>>()
        {
            { "kg", new Dictionary<string, double> {
                { "kg", 1.0 },
                { "lbs", 2.204623 },
                { "ounces", 35.27396 }
            } },
            { "lbs", new Dictionary<string, double> {
                { "kg", 0.4535924 },
                { "lbs", 1.0 },
                { "ounces", 16.0 }
            } },
            { "ounces", new Dictionary<string, double> {
                { "kg", 0.02834952 },
                { "lbs", 0.0625 },
                { "ounces", 1.0 }
            } }
        };


    public static void MapUnitConverterEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/convert/length/{value}/{fromUnit}/{toUnit}", (string value, string fromUnit, string toUnit) =>
        {
            double input;
            if (!double.TryParse(value, out input))
                return Results.BadRequest("Invalid value to convert. Please enter a number.");

            double factor = 0.0;
            switch (fromUnit.ToLower())
            {
                case "meters":
                    switch (toUnit.ToLower())
                    {
                        case "meters":
                            factor = lengthConversionFactors["meters"]["meters"];
                            break;
                        case "feet":
                            factor = lengthConversionFactors["meters"]["feet"];
                            break;
                        case "inches":
                            factor = lengthConversionFactors["meters"]["inches"];
                            break;
                    }
                    break;
                case "feet":
                    switch (toUnit.ToLower())
                    {
                        case "meters":
                            factor = lengthConversionFactors["feet"]["meters"];
                            break;
                        case "feet":
                            factor = lengthConversionFactors["feet"]["feet"];
                            break;
                        case "inches":
                            factor = lengthConversionFactors["feet"]["inches"];
                            break;
                    }
                    break;
                case "inches":
                    switch (toUnit.ToLower())
                    {
                        case "meters":
                            factor = lengthConversionFactors["inches"]["meters"];
                            break;
                        case "feet":
                            factor = lengthConversionFactors["inches"]["feet"];
                            break;
                        case "inches":
                            factor = lengthConversionFactors["inches"]["inches"];
                            break;
                    }
                    break;
            }
            if (factor == 0.0)
                return Results.BadRequest("Unsupported unit. Supported units are: meters, feet, inches.");

            return Results.Ok(input * factor);
        });

        app.MapGet("/convert/weight/{value}/{fromUnit}/{toUnit}", (string value, string fromUnit, string toUnit) =>
        {
            double input;
            if (!double.TryParse(value, out input))
                return Results.BadRequest("Invalid value to convert. Please enter a number.");

            double factor = 0.0;
            switch (fromUnit.ToLower())
            {
                case "kg":
                    switch (toUnit.ToLower())
                    {
                        case "kg":
                            factor = weightConversionFactors["kg"]["kg"];
                            break;
                        case "lbs":
                            factor = weightConversionFactors["kg"]["lbs"];
                            break;
                        case "ounces":
                            factor = weightConversionFactors["kg"]["ounces"];
                            break;
                    }
                    break;
                case "lbs":
                    switch (toUnit.ToLower())
                    {
                        case "kg":
                            factor = weightConversionFactors["lbs"]["kg"];
                            break;
                        case "lbs":
                            factor = weightConversionFactors["lbs"]["lbs"];
                            break;
                        case "ounces":
                            factor = weightConversionFactors["lbs"]["ounces"];
                            break;
                    }
                    break;
                case "ounces":
                    switch (toUnit.ToLower())
                    {
                        case "kg":
                            factor = weightConversionFactors["ounces"]["kg"];
                            break;
                        case "lbs":
                            factor = weightConversionFactors["ounces"]["lbs"];
                            break;
                        case "ounces":
                            factor = weightConversionFactors["ounces"]["ounces"];
                            break;
                    }
                    break;
            }
            if (factor == 0.0)
                return Results.BadRequest("Unsupported unit. Supported units are: kg, lbs, ounces.");

            return Results.Ok(input * factor);
        });
    }
}
