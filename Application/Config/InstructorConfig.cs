using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Application.Config
{
	public class InstructorConfig : IEntityTypeConfiguration<Instructor>
	{
		public void Configure(EntityTypeBuilder<Instructor> builder)
		{
			//Global 
			builder.ToTable("Instructors");
			//builder.Ignore(i => i.UserId);

			//Properties
			builder.Property(i => i.Specification).IsRequired(true).HasMaxLength(70);

			// Keys
			builder.HasKey(i => i.InstructorId);

			//Realationships (foreign keys)
			builder.HasOne(i => i.User).WithOne(u => u.Instructor).HasForeignKey<Instructor>(i => i.InstructorId).HasPrincipalKey<User>(u => u.Id).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(i => i.Departement).WithMany(d => d.Instructors).HasForeignKey(i => i.DepartementId);
			builder.HasMany(i => i.Sections).WithOne(sec => sec.Instructor).HasForeignKey(sec => sec.InstructorId).OnDelete(DeleteBehavior.NoAction);
			//indexs and constrains
		}
	}
}
