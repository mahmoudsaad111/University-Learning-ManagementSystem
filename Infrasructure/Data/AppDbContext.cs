using Application.Common.Interfaces.Presistance;
using Application.Config;
using Domain.Models;
using LearningPlatformTest.infrastructure.DatabaseConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace InfraStructure
{

	public class AppDbContext : IdentityDbContext<User,AppRole,int> , IAppDbContext
    {
		public AppDbContext()
        {
			//Configuration.LazyLoadingEnabled = false;
		}
		public AppDbContext(DbContextOptions dbContextOptions) :base (dbContextOptions)
        {

        }
        public DbSet<AcadimicYear> AcadimicYears { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamAnswer> ExamAnswers { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<FileResource> FileResourses { get; set; }  
        public DbSet<AssignmentResource> AssignmentResources { get; set; }
        public DbSet<AssignmentAnswerResource> AssignmentAnswerResources { get; set; }
        public DbSet<LectureResource> LectureResources { get; set; }
        public DbSet<Student_Lecture> Student_Lecture { get; set; }
        public DbSet<StudentNote> StudentNotes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseCycle> CourseCycles { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<StudentSection> StudentSections { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> postReplies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(DbSettings.MonsterDataBase); 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssignmentAnswerConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssignmentConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentReplyConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartementConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExamAnswerConfig).Assembly);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExamConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(FacultyConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(GroupConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(InstructorConfig).Assembly);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(LectureConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileResourceConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentSectionConfig).Assembly);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostReplyConfig).Assembly);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfessorConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentLectureConfig).Assembly);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentNoteConfig).Assembly);



			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SectionConfig).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof (CourseConfig).Assembly); 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseCategoryConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseCycleConfig).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcadimicYearConfig).Assembly);

            //
            // To include the Faculty always when call departement
             modelBuilder.Entity<Departement>().Navigation(D => D.Faculty).AutoInclude(true);
       
		}
    }
}

