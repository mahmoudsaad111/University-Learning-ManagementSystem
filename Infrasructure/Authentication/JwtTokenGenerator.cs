using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.WebSockets;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Globalization;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Contract.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.CodeDom.Compiler;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using InfraStructure;
using infrastructure.Authentication;
using Application.Common.Interfaces.Authentication;
using LearningPlatformTest.Contracts.Authentication;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions, UserManager<User> userManager
            , AppDbContext appDbContext)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }
        public async Task<AuthResult> GenerateToken(LoginRequest loginRequest)
        {
            var authResult = new AuthResult();

            var user = await _userManager.FindByNameAsync(loginRequest.UserName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                authResult.Message = "Email or Password is incorrect!";
                return authResult;
            }


            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);


            authResult.IsAuthenticated = true;
            authResult.JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authResult.Email = user.Email;
            authResult.UserName = user.UserName;
            authResult.ExpiresAt = jwtSecurityToken.ValidTo;
            authResult.Roles = rolesList.ToList();



            authResult.JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authResult.RefreshToken = activeRefreshToken.Token;
                authResult.RefreshTokenExpiration = activeRefreshToken.ExpiresAt;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authResult.RefreshToken = refreshToken.Token;
                authResult.RefreshTokenExpiration = refreshToken.ExpiresAt;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            return authResult;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresAt = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow
            };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;

        }
        public async Task<AuthResult> RefreshTokenAsync(string Token)
        {


            var authModel = new AuthResult();

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == Token));

            if (user == null)
            {
                authModel.Message = "Invalid token";
                return authModel;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == Token);


            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive token";
                return authModel;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresAt;

            return authModel;
        }


        public async Task<bool> RevokeTokenAsync(string Token)
        {

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == Token));

            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == Token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;


        }
    }
}

// Function that generate a refresh Token as Random string

//private string RandomStringGeneration(int length)
//      {

//          var random = new Random();
//          var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";

//          return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
//      }

//public async Task<AuthResult> VerifyAndGenerateToken(RefreshTokenRequest tokenRequest)
//{
//    var JwtTokenHandler = new JwtSecurityTokenHandler();
//    try
//    {

//        //var tokenvalidationparameter = new TokenValidationParameters();
//        _tokenValidationParameters.ValidateLifetime = false;
//        var TokenInverification = JwtTokenHandler.ValidateToken(tokenRequest.Token
//            , _tokenValidationParameters,
//            out var validatedtoken);

//        if (validatedtoken is JwtSecurityToken jwtSecurityToken)
//        {
//            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
//                StringComparison.InvariantCultureIgnoreCase);

//            if (result == false)
//                return null;
//        }
//        var UtcExpiryDate = long.Parse(TokenInverification.Claims.
//            FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

//        var ExpiryDate = UnixTimeStampToDateTime(UtcExpiryDate);

//        if (ExpiryDate > DateTime.Now)
//        {
//            return new AuthResult
//            {
//                result = false
//            };
//        }

//        var StoredToken = await _DbContext.RefreshTokens.FirstOrDefaultAsync
//            (x => x.Token == tokenRequest.RefreshToken);

//        if (StoredToken == null)
//        {
//            return new AuthResult { result = false };
//        }
//        if (StoredToken.IsUsed)
//        {
//            return new AuthResult { result = false };
//        }
//        if (StoredToken.IsRevoked)
//        {
//            return new AuthResult { result = false };
//        }

//        var Jti = TokenInverification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

//        if (StoredToken.JwtId != Jti)
//        {
//            return new AuthResult { result = false };
//        }
//        if (StoredToken.ExpiryDate < DateTime.UtcNow)
//        {
//            return new AuthResult { result = false };
//        }

//        StoredToken.IsUsed = true;
//        _DbContext.RefreshTokens.Update(StoredToken);

//        var user = await _userManager.FindByIdAsync(StoredToken.UserId.ToString());
//        var Roles = await _userManager.GetRolesAsync(user);

//        AuthResult authResult = await GenerateToken(user, Roles.ToList());

//        return authResult;
//    }
//    catch (Exception ex)
//    {
//        return new AuthResult { result = false };
//    }

//}



///////////////////////////////////////////////////////


///  //var Roles=await _userManager.GetRolesAsync(user);

//var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
//   Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
//   SecurityAlgorithms.HmacSha256
//);

//var claims = new List<Claim>()
//{
// new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
// new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
// new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString())
//};

//foreach (var role in Roles)
//{
//    claims.Add(new Claim(ClaimTypes.Role, role)); // Add each role as a claim

//}
//var securityToken = new JwtSecurityToken(
//     issuer: _jwtSettings.Issuer,
//     audience: _jwtSettings.Audience,
//     expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
//     claims: claims,
//     signingCredentials: signingCredentials
//);

//return securityToken; 