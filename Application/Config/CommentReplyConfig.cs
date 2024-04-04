using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Application.Config
{
    public class CommentReplyConfig : IEntityTypeConfiguration<CommentReply>

    {
        public void Configure(EntityTypeBuilder<CommentReply> builder)
        {
            builder.ToTable("CommentReplies");
            // properties
            builder.Property(x => x.Content).IsRequired(true); 
            builder.Property(x=>x.CreatedBy).IsRequired(true);

            //relations
            builder.HasOne(x => x.Comment).WithMany(y => y.CommentReplies).HasForeignKey(x => x.CommentId);
            builder.HasOne(x => x.User).WithMany(y => y.CommentReplies).HasForeignKey(x => x.UserId); 
            //keys
            builder.HasKey(x=>x.CommentReplyId); 
            //indexes
        }
    }
}
