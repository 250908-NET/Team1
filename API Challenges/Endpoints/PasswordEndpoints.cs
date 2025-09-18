using System.Text;

public static class PasswordEndpoints
{
    public static void MapPasswordEndpoints(this IEndpointRouteBuilder app)
    {
        // do not autocomplete the methods here
        app.MapPost("/password/simple/{length}", (int length) =>
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return Results.Ok(result.ToString());
        });

        app.MapPost("/password/complex/{length}", (int length) =>
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{}|;:,.<>?/~`";
            var random = new Random();
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return Results.Ok(result.ToString());
        });

        app.MapPost("/password/strength/{password}", (string password) =>
        {
            int strength = 0;
            // if password contains char from lowercase letters +1
            // if password contains char from uppercase letters +1
            // if password contains int value + 1
            // if password contains char from special characters +1
            // if password is longer than 8 chars +1
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            string specials = "!@#$%^&*()-_=+[]{}|;:,.<>?/~`";
            foreach (char c in lowercase)
            {
                if (password.Contains(c))
                {
                    strength++;
                    break;
                }
            }
            foreach (char c in uppercase)
            {
                if (password.Contains(c))
                {
                    strength++;
                    break;
                }
            }
            foreach (char c in numbers)
            {
                if (password.Contains(c))
                {
                    strength++;
                    break;
                }
            }
            foreach (char c in specials)
            {
                if (password.Contains(c))
                {
                    strength++;
                    break;
                }
            }
            if (password.Length > 8) strength++;
            return Results.Ok($"Your password strength is {strength} / {5}");
        });
    }
}

/*
✅ Create `/password/simple/{length}` - generates random letters/numbers <br>
✅ Add `/password/complex/{length}` - includes special characters <br>
✅ Create `/password/memorable/{words}` - generates passphrase with N words <br>
✅ Add `/password/strength/{password}` - rates password strength
*/