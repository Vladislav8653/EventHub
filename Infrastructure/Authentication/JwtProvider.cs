using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Contracts.AuthContracts;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{

    private readonly JwtOptions _options;
    private const int TokenSize = 32;
    
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    
    public string GenerateAccessToken(User user)
    {
        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        ];
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), 
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddSeconds(_options.ExpiresSeconds));
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[TokenSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }
    
    public ClaimsPrincipal GetClaimsAccessToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        return principal;
    }

    public bool ValidateRefreshToken(string? originalTrueToken, string userToken, DateTime? expiresTime)
    {
        if (string.IsNullOrEmpty(originalTrueToken))
            throw new ArgumentNullException(nameof(originalTrueToken),"User does not have a refresh token");
        if (originalTrueToken != userToken)
            throw new SecurityTokenException("Refresh token is invalid or has been revoked.");
        if (expiresTime == null)
            throw new SecurityTokenException("Refresh token expires time is missing.");
        if(expiresTime < DateTime.UtcNow)
            throw new SecurityTokenException("Refresh token has expired.");
        return true;
    }

    public Guid GetUserIdAccessToken(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentNullException(nameof(token), "User does not have a access token");
        }
        var claims = GetClaimsAccessToken(token);
        var userIdClaim = claims.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new SecurityTokenException("User ID claim is missing in the token.");
        }
        if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
            throw new SecurityTokenException("User ID claim is invalid.");
        return userId;
    }
}