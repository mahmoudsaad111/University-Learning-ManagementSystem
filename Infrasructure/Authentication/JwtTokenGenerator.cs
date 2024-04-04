using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt; 
using LearningPlatformTest.Application.Common.Authentication;
using System.Security.Claims;
using System.Net.WebSockets;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace LearningPlatformTest.infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
      
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator( IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        public string GenerateToken(int ID, List<string> Roles)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new List<Claim>()
            {
             new Claim(JwtRegisteredClaimNames.Sub,ID.ToString()),
            };

            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Add each role as a claim
            }
            var securityToken = new JwtSecurityToken(
                 issuer: _jwtSettings.Issuer,
                 audience: _jwtSettings.Audience,   
                 expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes), 
                 claims: claims,
                 signingCredentials: signingCredentials
            ); 
           
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
