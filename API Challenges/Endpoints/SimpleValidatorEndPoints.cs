public static class CalculatorEndpoints
{
    public static void SimpleValidatorEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/validate/email/{email}", (HttpContext context, string inp) =>
        {
            bool[] checklist = {
                inp.Contains("@"),
                inp.Contains("."),
                inp.lastIndexOf(".") > inp.IndexOf("@"),
                inp.lastIndexOf(".") < inp.Length - 1,
                inp.IndexOf("@") > 0
            };

            foreach (var check in checklist)
            {
                if (!check)
                {
                    return Results.BadRequest(new { error = "Invalid email format." });
                }
            }

            return Result.Ok(new
            {
                email = inp,
                isValid = true
            });
        });
        app.MapGet("/validate/phone/{phone}", (HttpContext context, string inp) =>
        {
            bool[] checklist = {
                inp.Length == 10,
                inp.All(char.IsDigit),
                inp.IndexOfAny( new char('-').length >= 3 && inp.indexOfAny(new char('-').length <= 6))
            };
            foreach (var check in checklist)
            {
                if (!check)
                {
                    return Results.BadRequest(new { error = "Invalid phone number format." });
                }
            }
            return Result.Ok(new
            {
                phone = inp,
                isValid = true
            });
        });
        app.MapGet("/validate/creditcard/{number}", (HttpContext context, string inp) =>
        {
            bool[] checklist = {
                inp.Length == 16,
                inp.All(char.IsDigit),
                !inp.Contains("-")
            };
            foreach (var check in checklist)
            {
                if (!check)
                {
                    return Results.BadRequest(new { error = "Invalid credit card format." });
                }
            }
            return Result.Ok(new
            {
                creditCard = inp,
                isValid = true
            });
        });
        app.MapGet("/validate/strongpassword/{password}", (HttpContext context, string inp) =>
        {
            bool[] checklist = {
                inp.Length >= 8,
                inp.Any(char.IsUpper),
                inp.Any(char.IsLower),
                inp.Any(char.IsDigit),
                inp.Any(ch => !char.IsLetterOrDigit(ch))
            };
            foreach (var check in checklist)
            {
                if (!check)
                {
                    return Results.BadRequest(new { error = "Weak password." });
                }
            }
            return Result.Ok(new
            {
                password = inp,
                isValid = true
            });
        });
    }
}