﻿namespace Shared;

public class LoginModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
    public string? ConfirmPassword { get; set; }
}