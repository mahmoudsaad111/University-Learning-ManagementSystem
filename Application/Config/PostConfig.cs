using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace Application.Config
{
	public class PostConfig : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			//Prop
			builder.Property(p => p.Content).IsRequired(true).HasMaxLength(500);
			builder.Property(p => p.CreatedBy).IsRequired(false).HasMaxLength(50);
			//& update SectionId and CourseCycleId to IsRequired(fasle)
			builder.Property(p => p.SectionId).IsRequired(false);
			builder.Property(p=>p.CourseCycleId).IsRequired(false);


			//Keys
			builder.HasKey(p => p.PostId);

			//Relations
			//builder.HasOne(p => p.Section).WithMany(sec => sec.Posts).HasForeignKey(p => p.SectionId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(p => p.Publisher).WithMany(user => user.Posts).HasForeignKey(p => p.PublisherId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(p => p.CourseCycle).WithMany(cc => cc.Posts).HasForeignKey(p => p.CourseCycleId).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(p => p.ReplaysOnPost).WithOne(rp => rp.Post).HasForeignKey(rp => rp.PostId).OnDelete(DeleteBehavior.Cascade);

			// Indexes
			builder.HasIndex(p => p.SectionId).IsUnique(false);
			builder.HasIndex(p => p.CourseCycleId).IsUnique(false);
		}
	}

}
