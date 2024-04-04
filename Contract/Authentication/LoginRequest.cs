using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatformTest.Contracts.Authentication
{
    public record LoginRequest
    {
        public string? Email { get; set; } = null!; 
        public string? Password { get; set; } = null!;
    }
}
