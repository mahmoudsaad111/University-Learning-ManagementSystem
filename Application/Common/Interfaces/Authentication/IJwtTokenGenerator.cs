using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatformTest.Application.Common.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int ID,List<string> Roles);
    }
}
