using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace Application.Config
{
	public class SectionConfig : IEntityTypeConfiguration<Section>
	{
		public void Configure(EntityTypeBuilder<Section> builder)
		{
			// Properties
			builder.Property(sec => sec.Name).IsRequired(true).HasMaxLength(100);
			builder.Property(sec => sec.Description).IsRequired(true).HasMaxLength(250);

			//Keys
			builder.HasKey(sec => sec.SectionId);

            //Relationships (Foreign Keys)

            //& another update to Post with Section config to make the relation is not required 

            builder.HasMany(sec => sec.Posts).WithOne(p => p.Section).HasForeignKey(p => p.SectionId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(sec => sec.StudentsInSection).WithOne(sis => sis.Section).HasForeignKey(sis => sis.SectionId).OnDelete(DeleteBehavior.Cascade);

            //& another update to Post with Section config to make the relation is not required 

            builder.HasMany(sec=>sec.Lectures).WithOne(l => l.Section).HasForeignKey(l=>l.SectionId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);	
			//builder.HasOne(sec => sec.Instructor).WithMany(i => i.Sections).HasForeignKey(sec => sec.SectionId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(sec => sec.CourseCycle).WithMany(cc => cc.Sections).HasForeignKey(sec => sec.CourseCycleId).OnDelete(DeleteBehavior.NoAction);

			//& Add Relation between Section and assignment after delete the relation betweemn Lecture and assignment
			builder.HasMany(sec=>sec.Assignments).WithOne(ass=>ass.Section).HasForeignKey(ass=>ass.SectionId).IsRequired(true).OnDelete(DeleteBehavior.Cascade); 
			//Indexes and constrains
			
			//remove this constrain 
		//$$	builder.HasIndex(sec => new { sec.InstructorId, sec.CourseCycleId }).IsUnique(true);

		}
	}

}
