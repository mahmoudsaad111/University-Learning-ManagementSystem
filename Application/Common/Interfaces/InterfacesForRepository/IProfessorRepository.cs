﻿using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IProfessorRepository :IBaseRepository<Professor>
    {
        public Task<IEnumerable<ReturnedProfessorDto>> GetAllProfessorsInDepartement(int DeptId );
        public Task<IEnumerable<NameIdDto>> GetLessInfoProfessorByDeptId(int DeptId);
    }
}
