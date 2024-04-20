
namespace Infrastructure.Common
{
	using Application.Common.Interfaces.InterfacesForRepository;
	using Application.Common.Interfaces.Presistance;
	using Domain.Models;   
    using Infrastructure.Repositories;
	using InfraStructure;
   
    public class UnitOfwork : IUnitOfwork
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;
		public IAppDbContext Context => _context;
		public IUserRepository UserRepository { get; private set; }
		public IBaseRepository<Student> StudentRepository { get; private set; }
		public IBaseRepository<Professor> ProfessorRepository { get; private set; }
		public IBaseRepository<Instructor> InstructorRepository { get; private set; }
        public IFacultyRepository FacultyRepository { get; private  set; }

        public IDepartementRepository  DepartementRepository { get; private set; }

		public IGroupRepository GroupRepository { get; private set; }
		public ISectionRepository SectionRepository { get; private set; }
		public ICourseRepository CourseRepository { get; private set; }
        public ICourseCategoryRepository CourseCategoryRepository { get; private set; }
        public ICourseCycle CourseCycleRepository { get; private set; }
        public IBaseRepository<Lecture> LectureRepository { get; private set; }
        public IBaseRepository<Assignment> AssignementRepository {  get; private set; }    
        public IBaseRepository<AssignmentAnswer> AssignementAnswerRepository { get; private set; }
        public ILectureResourceRepository LectureResourceRepository { get; private set; }
        public IAssignementResourceRepository AssignementResourceRepository { get; private set; }
        public IAssignementAnswerResouceRepository AssignementAnswerResouceRepository { get; private set; }
        public IBaseRepository<Post> PostRepository {get;private set;}
        public IBaseRepository<PostReply> PostReplyRepository {get;private set;}
        public IBaseRepository<Comment> CommentRepository { get;private set;}
        public IBaseRepository<CommentReply> CommentReplyRepository { get;private set;} 
        public IAcadimicYearRepository AcadimicYearRepository { get;private set; }
        public IFileResourceRepository FileResourceRepository { get;private set; }
        public UnitOfwork(AppDbContext context)
        {
            _context = context;
            StudentRepository = new StudentRepository (_context);
			ProfessorRepository = new ProfessorRepository (_context);
			InstructorRepository = new InstructorRepository(_context);
			FacultyRepository = new FacultyRepository(_context);
            UserRepository = new UserRepository(_context);
            GroupRepository = new GroupRepository(_context);
            CourseRepository = new CourseRepository(_context);
            SectionRepository = new SectionRepository(_context);
            DepartementRepository= new DepartementRepository(_context);
			CourseCategoryRepository = new CourseCategoryRepository(_context);
            CourseCycleRepository = new CourseCycleRepository(_context);
            LectureRepository = new LectureRepository(_context);
            LectureResourceRepository=new LectureResourceRepository (_context);
            PostRepository = new PostRepository(_context);
            PostReplyRepository= new PostReplyRepository(_context);
            CommentRepository = new CommentRepository(_context);
            CommentReplyRepository=new CommentReplyRepository(_context);
            AcadimicYearRepository = new AcadimicYearRepository(_context);
            FileResourceRepository= new FileResourceRepository(_context);
            AssignementRepository =new AssignementRepository(_context);
            AssignementAnswerRepository=new AssignementAnswerRepository(_context);
        }

		public async Task<int> SaveChangesAsync()
        {
           
            bool IsTrcked=_context.ChangeTracker.HasChanges();
            ;
            return await _context.SaveChangesAsync();
		}
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
