using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Inkukan.Application.Services.Implementations
{
    public class TokenConfiguration
    {
        public required string SecretKey { get; set; }
        public required string Issuer { get; set; }
        public required int ValidityInHours { get; set; }
    }

    public class TokenService(IRoleRepository roleRepository, IOptions<TokenConfiguration> tokenOptions) : ITokenService
    {
        public async Task<string> GetTokenForUserAsync(User user, CancellationToken cancellationToken)
        {
            JsonWebTokenHandler handler = new();
            byte[] key = Encoding.ASCII.GetBytes(tokenOptions.Value.SecretKey);
            SigningCredentials credentials = new(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = await GenerateClaims(user, cancellationToken),
                Expires = DateTime.UtcNow.AddHours(tokenOptions.Value.ValidityInHours),
                SigningCredentials = credentials,
                Issuer = tokenOptions.Value.Issuer,
                IssuedAt = DateTime.UtcNow,
            };

            return handler.CreateToken(tokenDescriptor);
        }

        private async Task<ClaimsIdentity> GenerateClaims(User user, CancellationToken cancellationToken)
        {
            ClaimsIdentity claims = new();
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email!));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName!));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            IEnumerable<string> userRoles = await roleRepository.GetUserRolesAsync(user);

            claims.AddClaims(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
