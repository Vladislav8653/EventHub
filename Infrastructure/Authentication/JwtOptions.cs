﻿namespace Infrastructure.Authentication;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public int ExpiresSeconds { get; set; }
}