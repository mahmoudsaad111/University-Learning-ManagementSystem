﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IPostSectionInClient
    {
        public Task RecievePost(string PostContent);
    }
}
