﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AssignmentResource : FileResource
    {
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}