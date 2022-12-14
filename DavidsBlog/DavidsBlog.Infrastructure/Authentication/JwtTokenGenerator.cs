using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DavidsBlog.Application.Common.Interfaces.Authentication;
using DavidsBlog.Application.Common.Interfaces.Services;
using DavidsBlog.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DavidsBlog.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider provider;
    private readonly JwtSettings jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider provider, IOptions<JwtSettings> jwtOptions)
    {
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        this.jwtSettings = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            expires: provider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes), 
            claims: claims, 
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}