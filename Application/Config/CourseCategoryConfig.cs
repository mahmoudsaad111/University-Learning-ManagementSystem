using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace Application.Config
{
	public class CourseCategoryConfig : IEntityTypeConfiguration<CourseCategory>
	{
		public void Configure(EntityTypeBuilder<CourseCategory> builder)
		{
			// Propreties
			builder.Property(Cc => Cc.Name).IsRequired(true).HasMaxLength(100);
			builder.Property(Cc => Cc.Description).IsRequired(false).HasMaxLength(250) ;

			// Keys
			builder.HasKey(Cc => Cc.CourseCategoryId);

			//Relationships 
			builder.HasMany(Cc => Cc.Courses).WithOne(c => c.CourseCategory).HasForeignKey(c => c.CourseCategoryId).OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(Cc => Cc.Departement).WithMany(d => d.CoursesCategories).HasForeignKey(Cc => Cc.DepartementId);
		}
	}

}
