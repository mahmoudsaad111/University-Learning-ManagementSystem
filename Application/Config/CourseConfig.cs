using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace Application.Config
{
	public class CourseConfig : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			//properties
			builder.Property(c=>c.Name).IsRequired(true).HasMaxLength(100);
			builder.Property(c=>c.Description).IsRequired(false).HasMaxLength(250);
			builder.Property(c => c.TotalMark).IsRequired(true);
			builder.Property(c => c.AcadimicYearId).IsRequired(true);
			builder.Property(c => c.CourseCategoryId).IsRequired(true);

			// Keys
			builder.HasKey(c => c.CourseId);

			//Relationships
			//builder.HasOne(c => c.CourseCategory).WithMany(Cc => Cc.Courses).HasForeignKey(c=>c.CourseCategoryId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(c => c.Departement).WithMany(d => d.Courses).HasForeignKey(c => c.DepartementId).OnDelete(DeleteBehavior.NoAction);

			builder.HasMany(c => c.CourseCycles).WithOne(cc => cc.Course).HasForeignKey(cc => cc.CourseId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(c => c.AcadimicYear).WithMany(ay => ay.Courses).HasForeignKey(c => c.AcadimicYearId).OnDelete(DeleteBehavior.NoAction);
			//Indexes and Constrains


		}
	}

}
