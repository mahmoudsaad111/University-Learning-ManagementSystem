using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Infrastructure.RealTimeServices
{
    public class CheckDataOfRealTimeRequests : ICheckDataOfRealTimeRequests
    {
        private readonly IUnitOfwork unitOfwork;

        public CheckDataOfRealTimeRequests(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<bool> CheckIfUserInGroup(int UserId, int GroupId, TypesOfUsers typesOfUsers)
        {
            if (typesOfUsers == TypesOfUsers.Student)
                return await unitOfwork.StudentRepository.CheckIfStudentInGroup(UserId, GroupId);
            return false;
        }
        public async Task<bool> CheckIfUserInCourseCycle(int CourseCycleId, int UserId, TypesOfUsers typesOfUsers)
        {
            if (typesOfUsers == TypesOfUsers.Student)
                return await unitOfwork.StudentCourseCycleRepository.ChekcIfStudentInCourseCycle(UserId, CourseCycleId);
            else if (typesOfUsers == TypesOfUsers.Professor)
                return await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(UserId, CourseCycleId);
            else if (typesOfUsers == TypesOfUsers.Instructor)
                return await unitOfwork.CourseCycleRepository.CheckIfInstructorInCoursecycle(UserId, CourseCycleId);

            return false;
        }

        public async Task<bool> CheckIfUserInSection(int SectionId, int UserId, TypesOfUsers typesOfUsers)
        {
            if (typesOfUsers == TypesOfUsers.Student)
                return await unitOfwork.StudentSectionRepository.CheckIfStudentInSection(StudentId: UserId, SectionId: SectionId);

            else if (typesOfUsers == TypesOfUsers.Professor)
                return await unitOfwork.ProfessorRepository.CheckIfProfessorInSection(ProfessorId: UserId, SectionId: SectionId);

            else if (typesOfUsers == TypesOfUsers.Instructor)
                return await unitOfwork.SectionRepository.CheckIfInstructorInSection(InstrucotrId: UserId, SectionId: SectionId);

            return false;
        }

        public async Task<Tuple<TypesOfUsers, User>> GetTypeOfUserAndHisId(string userName)
        {
            var user = await unitOfwork.UserRepository.GetUserByUserName(userName);

            if (user == null)
                return null;

            if (user.Student is not null)
                return new Tuple<TypesOfUsers, User>(TypesOfUsers.Student, user);

            else if (user.Professor is not null)
                return new Tuple<TypesOfUsers, User>(TypesOfUsers.Professor, user);

            else if (user.Instructor is not null)
                return new Tuple<TypesOfUsers, User>(TypesOfUsers.Instructor, user);

            return null;

        }
    }
}
