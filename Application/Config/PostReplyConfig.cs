using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace Application.Config
{
	public class PostReplyConfig : IEntityTypeConfiguration<PostReply>
	{
		public void Configure(EntityTypeBuilder<PostReply> builder)
		{
			//Prop
			builder.Property(pr => pr.Content).IsRequired(true).HasMaxLength(500);
			builder.Property(pr=>pr.CreatedBy).IsRequired(false).HasMaxLength(50);
			//Key
			builder.HasKey(pr => pr.PostReplyId);

			//Relations
			//builder.HasOne(rp => rp.Post).WithMany(p => p.ReplaysOnPost).HasForeignKey(rp => rp.PostId).OnDelete(DeleteBehavior.Cascade);
			//builder.HasOne(rp=>rp.Replier).WithMany(u=>u.ReplaysOnPosts).HasForeignKey(rp=>rp.ReplierId).OnDelete(DeleteBehavior.Cascade);	

		}
	}

}
