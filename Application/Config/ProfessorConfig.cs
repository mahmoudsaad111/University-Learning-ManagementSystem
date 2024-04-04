using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Application.Config
{
	public class ProfessorConfig : IEntityTypeConfiguration<Professor>
	{
		public void Configure(EntityTypeBuilder<Professor> builder)
		{
			//Global 
			builder.ToTable("Professors");
			//builder.Ignore(p => p.UserId);

			//Properties
			builder.Property(p => p.Specification).IsRequired(true).HasMaxLength(70);

			// Keys
			builder.HasKey(p => p.ProfessorId);

			//Realationships (foreign keys)
			builder.HasOne(p => p.User).WithOne(u => u.Professor).HasForeignKey<Professor>(p => p.ProfessorId).HasPrincipalKey<User>(u => u.Id).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(p => p.CoursesCycles).WithOne(Cc => Cc.Professor).HasForeignKey(Cc => Cc.ProfessorId).OnDelete(DeleteBehavior.NoAction);
			

			//builder.HasOne(p=>p.Departement).WithMany(d=>d.Professors).HasForeignKey(p=>p.DepartementId);
			//indexs and constrains
		}

	}
}
