namespace Application.Common.Interfaces.Presistance
{
	using Application.Common.Interfaces.InterfacesForRepository;
	using Domain.Models;
	using Microsoft.EntityFrameworkCore;
	using System;

    public interface IUnitOfwork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public IProfessorRepository ProfessorRepository { get; }
        public IInstructorRepository InstructorRepository { get; }
        public IStudentSectionRepository StudentSectionRepository { get; }
        public IFacultyRepository FacultyRepository { get; }
        public IDepartementRepository DepartementRepository { get; }
        public IGroupRepository GroupRepository { get; }
        public ISectionRepository SectionRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public ICourseCategoryRepository CourseCategoryRepository { get; }
        public ICourseCycle CourseCycleRepository { get; }
        public IBaseRepository<Lecture> LectureRepository { get; }
        public ILectureResourceRepository LectureResourceRepository { get; }
        public IAssignementResourceRepository AssignementResourceRepository { get; }
        public IAssignementAnswerResouceRepository AssignementAnswerResouceRepository { get; }
        public IBaseRepository<Assignment> AssignementRepository { get; }
        public IBaseRepository<AssignmentAnswer> AssignementAnswerRepository { get; }

        public IBaseRepository<Post> PostRepository { get; }
        public IBaseRepository<PostReply> PostReplyRepository { get; }
        public IBaseRepository<Comment> CommentRepository { get; }
        public IBaseRepository<CommentReply> CommentReplyRepository { get; }
        public IAcadimicYearRepository AcadimicYearRepository { get; }
        public IFileResourceRepository FileResourceRepository { get; }

        public IStudentCourseCycleRepository StudentCourseCycleRepository { get; }

        public IExamRepository ExamRepository { get; }
        public IExamAnswerRepository ExamAnswerRepository { get;}
        public IExamPlaceRepository ExamPlaceRepository { get; } 
        public IStudentExamRepository StudentExamRepository { get; }
        public IMCQRepository MCQRepository { get; }    
        public ITFQRepository TFQRepository { get; }
        public IStudentAnswerInTFQRepository StudentAnswerInTFQRepository { get; }
        public IStudentAnswerInMCQRepository StudentAnswerInMCQRepository { get; }

        public IAppDbContext Context { get; }
        public Task<int> SaveChangesAsync();
    }
}
