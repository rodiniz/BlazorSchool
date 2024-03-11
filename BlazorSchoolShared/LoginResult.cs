namespace Shared;

public class LoginResult
{
    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public bool Successful { get; set; }
    public string? Error { get; set; }
}