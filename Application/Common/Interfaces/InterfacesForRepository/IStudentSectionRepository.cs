﻿using Application.Common.Interfaces.Presistance;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IStudentSectionRepository : IBaseRepository<StudentSection>
    {
        public Task<IEnumerable<int>> GetAllSectionsIdofStudent(int StudentId);
        public Task<bool> CheckIfStudentInSection(int StudentId, int SectionId); 
        
        public Task<int> GetStudentSectionId(int SectionId, int StudentCourseCycleId);
    }
}