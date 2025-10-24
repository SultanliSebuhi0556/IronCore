using IronCore.BL.ExternalServices.Instances;
using IronCore.CORE.Entities;
using IronCore.CORE.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IronCore.BL.ExternalServices.Implements;
public class TokenGenerator : ITokenGenerator
{
    readonly JWTOptions _option;
    public TokenGenerator(IOptions<JWTOptions> options)
    {
        _option = options.Value;
    }

    public string CreateJWTToken(AppUser user, int hours = 24)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, string.Empty),
            new Claim("AvatarUrl", user.ProfileImageUrl),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.SecretKey));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken securityToken = new(
            issuer: _option.Issuer,
            audience: _option.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(hours),
            signingCredentials: credentials
        );
        JwtSecurityTokenHandler handler = new();
        return handler.WriteToken(securityToken);
    }
}