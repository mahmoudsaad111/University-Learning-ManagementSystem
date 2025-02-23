﻿using Application.Common.Interfaces.Presistance;
using Contract.Dto.Assignements;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IAssignementRepository : IBaseRepository<Assignment>
    {
        public Task<IEnumerable<Assignment>> GetAllAssignementsOfSection(int sectionId);
        public Task<IEnumerable<string>> GetEmailsOfStudnetsHavingAccessToAssignment(int AssignmentId);
    }
}
