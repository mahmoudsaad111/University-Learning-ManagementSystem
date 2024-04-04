using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace Application.Config
{
	public class CourseCycleConfig : IEntityTypeConfiguration<CourseCycle>
	{
		public void Configure(EntityTypeBuilder<CourseCycle> builder)
		{
			//Properties
			builder.Property(cc => cc.Title).IsRequired(true).HasMaxLength(100);

			//Keys
			builder.HasKey(cc => cc.CourseCycleId);

			//Relations
			//& another update to Post with CourseCylce config to make the relation is not required 
			builder.HasMany(cc => cc.Posts).WithOne(p => p.CourseCycle).HasForeignKey(p => p.CourseCycleId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(cc=>cc.Sections).WithOne(sec=>sec.CourseCycle).HasForeignKey(sec=>sec.CourseCycleId).OnDelete(DeleteBehavior.Cascade);
			//builder.HasOne(cc => cc.Group).WithMany(g => g.CourseCycles).HasForeignKey(cc => cc.GroupId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(cc => cc.Course).WithMany(c => c.CourseCycles).HasForeignKey(cc => cc.CourseId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(cc => cc.Exams).WithOne(e => e.CourseCycle).HasForeignKey(e => e.CourseCycleId).OnDelete(DeleteBehavior.Cascade);

            //& another update to Post with CourseCylce config to make the relation is not required 

            builder.HasMany(cc => cc.Lectures).WithOne(l => l.CourseCycle).HasForeignKey(l => l.CourseCycleId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(Cc => Cc.Professor).WithMany(p => p.CoursesCycles).HasForeignKey(Cc => Cc.ProfessorId).OnDelete(DeleteBehavior.NoAction);
			//Indexes and Constrains
		 
			builder.HasIndex(cc => new { cc.GroupId, cc.CourseId }).IsUnique(true);
		}
	}

}
