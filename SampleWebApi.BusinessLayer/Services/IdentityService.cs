using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleWebApi.BusinessLayer.Settings;
using SampleWebApi.Shared.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.BusinessLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings jwtSettings;

        public IdentityService(IOptions<JwtSettings> jwtSettingsOptions)
        {
            jwtSettings = jwtSettingsOptions.Value;
        }

        public Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            if (request.Username == request.Password)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim(ClaimTypes.Role, "Developer")
                };

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddDays(3),
                    signingCredentials: signingCredentials
                );

                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                var result = new AuthResponse { AccessToken = accessToken };
                return Task.FromResult(result);
            }

            return Task.FromResult<AuthResponse>(null);
        }
    }
}
