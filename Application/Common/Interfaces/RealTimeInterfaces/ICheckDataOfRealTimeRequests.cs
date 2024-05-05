using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface ICheckDataOfRealTimeRequests
    {
        public Task<Tuple<TypesOfUsers, User>> GetTypeOfUserAndHisId(string userName);
        public Task<bool> CheckIfUserInSection(int SectionId, int UserId, TypesOfUsers typesOfUsers);

        public Task<bool> CheckIfUserInCourseCycle(int CourseCycleId, int UserId, TypesOfUsers typesOfUsers);
        public Task<bool> CheckIfUserInGroup(int UserId, int GroupId, TypesOfUsers typesOfUsers);


    }
}
