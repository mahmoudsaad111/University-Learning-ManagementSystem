namespace Application.Common.Interfaces.Presistance
{
	using Application.Common.Interfaces.InterfacesForRepository;
	using Domain.Models;
	using Microsoft.EntityFrameworkCore;
	using System;

    public interface IUnitOfwork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IBaseRepository<Student> StudentRepository { get; }
        public IBaseRepository<Professor> ProfessorRepository { get; }
        public IBaseRepository<Instructor> InstructorRepository { get; }
        public IFacultyRepository FacultyRepository { get; }
        public IDepartementRepository  DepartementRepository { get; }
		public IGroupRepository  GroupRepository { get; }
		public ISectionRepository SectionRepository { get; }
		public ICourseRepository CourseRepository { get; }
        public IBaseRepository<CourseCategory> CourseCategoryRepository { get; }
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
        public IAppDbContext Context { get; }
        public Task<int> SaveChangesAsync();
    }
}
