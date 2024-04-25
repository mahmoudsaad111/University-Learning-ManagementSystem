using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace Application.Config
{
	public class StudentSectionConfig : IEntityTypeConfiguration<StudentSection>
	{
		public void Configure(EntityTypeBuilder<StudentSection> builder)
		{
			// Proprties
			builder.Property(ss => ss.StudentTotalMarks).HasDefaultValue(0);
			builder.Property(ss => ss.StudentCourseCycleId).IsRequired(true);
			builder.Property(ss=>ss.SectionId).IsRequired(true);


			//Keys 
			builder.HasKey(ss => ss.StudentSectionId);

			//Relations
			//builder.HasOne(ss => ss.Student).WithMany(s => s.StudentsInSection).HasForeignKey(ss => ss.StudentId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(ss => ss.Section).WithMany(sec => sec.StudentsInSection).HasForeignKey(ss => ss.SectionId).OnDelete(DeleteBehavior.NoAction);

			// Indexes and Constrains
			builder.HasIndex(ss => new { ss.StudentCourseCycleId, ss.SectionId }).IsUnique(true);
		}
	}

}
