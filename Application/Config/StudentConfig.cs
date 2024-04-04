using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Config
{
	public class StudentConfig : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			//Global 
			builder.ToTable("Students");
			//builder.Ignore(s => s.UserId);

			//Properties
			builder.Property(s => s.GPA).IsRequired(true);
			builder.Property(s => s.AcadimicYear).IsRequired(true);
			builder.Property(s => s.GroupId).IsRequired(true).HasDefaultValue(0);
			builder.Property(s => s.DepartementId).IsRequired(true);
			
			// Keys
			builder.HasKey(s => s.StudentId);
		 
			//Realationships (foreign keys)
			builder.HasOne(s => s.User).WithOne(u => u.Student).HasForeignKey<Student>(s => s.StudentId).HasPrincipalKey<User>(u => u.Id).OnDelete(DeleteBehavior.Cascade);
			//builder.HasOne(s => s.Group).WithMany(g => g.Students).HasForeignKey(s => s.GroupId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(s => s.Departement).WithMany(d => d.Students).HasForeignKey(s => s.DepartementId);
			builder.HasMany(s => s.StudentsInSection).WithOne(sis => sis.Student).HasForeignKey(sis => sis.StudentId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(s=>s.AssignmentAnswers).WithOne(asa=>asa.Student).HasForeignKey(asa => asa.StudentId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(s => s.StudentNotes).WithOne(sn => sn.Student).HasForeignKey(sn => sn.StudentId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(s => s.ExamAnswers).WithOne(exa => exa.Student).HasForeignKey(exa => exa.StudentId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(s=>s.StudentLecture).WithOne(sl=>sl.Student).HasForeignKey(sl => sl.StudentId).OnDelete(DeleteBehavior.Cascade);

			//indexs and constrains
			builder.HasIndex(s => s.GroupId).IsUnique(false);
		}
	}
}
