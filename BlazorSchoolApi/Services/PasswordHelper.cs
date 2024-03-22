namespace BlazorSchoolApi.Services;

public static class PasswordHelper
{
    private static readonly Random Random = new();
    private const string LowerCase = "abcdefghijklmnopqursuvwxyz";
    private const string UpperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "1234567890";
    public static string GenerateStrongPassword(int passwordSize)
    {
        if (passwordSize < 8)
            throw new ArgumentException("The password size must be equals or greater than 8.", nameof(passwordSize));

        var numberOfLowerCase = Random.Next(1, passwordSize - 4);
        var numberOfUpperCase = Random.Next(1, passwordSize - numberOfLowerCase - 2);
        var numberOfNumbers = passwordSize - numberOfLowerCase - numberOfUpperCase;

        var lowerCaseCharacters = GetRandomString(LowerCase, numberOfLowerCase);
        var upperCaseCharacters = GetRandomString(UpperCases, numberOfUpperCase);
        var numberCharacters = GetRandomString(Numbers, numberOfNumbers);

        var password = $"{lowerCaseCharacters}{upperCaseCharacters}{numberCharacters}";

        return new string(password.ToCharArray().OrderBy(_ => Random.Next(2) % 2 == 0).ToArray());
    }

    private static string GetRandomString(string chars, int length)
    {
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}