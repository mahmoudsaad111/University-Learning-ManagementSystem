using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatformTest.Contracts.Authentication
{
    public record LoginResponse
    {
        public int ID { get; set; }
        public string? Name { get; set; } = null!;
        public string? Email { get; set; } = null!;

        public string? Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

    }
}
