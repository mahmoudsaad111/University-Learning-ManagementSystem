using Contract.Authentication;
using Domain.Models;
using LearningPlatformTest.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<AuthResult> GenerateToken(LoginRequest loginRequest);
        Task<AuthResult> RefreshTokenAsync(string Token);

        Task<bool> RevokeTokenAsync(string Token);
        // Task<AuthResult> VerifyAndGenerateToken(RefreshTokenRequest TokenRequest);  
    }
}
