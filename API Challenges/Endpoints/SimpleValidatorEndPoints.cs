public static class SimpleValidatorEndPoints
{
    public static void MapSimpleValidatorEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/validate/email/{email}", (HttpContext context, string inp) =>
        {
            bool[] checklist = {
                inp.Contains("@"),
                inp.Contains("."),
                inp.LastIndexOf(".") > inp.IndexOf("@"),
                inp.LastIndexOf(".") < inp.Length - 1,
                inp.IndexOf("@") > 0
            };

            foreach (var check in checklist)
            {
                if (!check)
                {
                    return Results.BadRequest(new { error = "Invalid email format." });
                }
            }

            return Results.Ok(new
            {
                email = inp,
                isValid = true
            });
        });
        app.MapGet("/validate/phone/{phone}", (HttpContext context, string inp) =>
        {
            bool[] checklist = {
                inp.Length == 10,
                inp.All(char.IsDigit)
            };
            foreach (var check in checklist)
            {
                if (!check)
                {
                    return Results.BadRequest(new { error = "Invalid phone number format." });
                }
            }
            return Results.Ok(new
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
            return Results.Ok(new
            {
                creditCard = inp,
                isValid = true
            });
        });
        app.MapGet("/validate/strongpassword/{password}", (HttpContext context, string inp) =>
        {
            bool[] checklist = {
                inp.Length >= 8,
                inp.Any(char.IsUpper) && inp.Any(char.IsLower) && inp.Any(char.IsDigit)
            };
            foreach (var check in checklist)
            {
                if (!check)
                {
                    return Results.BadRequest(new { error = "Weak password." });
                }
            }
            return Results.Ok(new
            {
                password = inp,
                isValid = true
            });
        });
    }
}