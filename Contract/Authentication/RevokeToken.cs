using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Authentication
{
    public interface RevokeToken
    {
        public string? Token { get; set; }
    }
}
