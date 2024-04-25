using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface ICheckDataOfRealTimeRequests
    {
        public Task<Tuple<TypesOfUsers,int>> GetTypeOfUserAndHisId(string userName);
        public Task<bool> CheckIfUserInSection( int SectionId , int UserId,TypesOfUsers typesOfUsers);
    }
}
