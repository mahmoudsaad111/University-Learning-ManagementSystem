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
    public class CommentConfig: IEntityTypeConfiguration<Comment>

    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            // properties
            builder.Property(x => x.Content).IsRequired(true); 
            builder.Property(x=>x.CreatedBy).IsRequired(true);

            //keys
            builder.HasKey(x => x.CommentId); 


            //relations
            builder.HasMany(x => x.CommentReplies).WithOne(y => y.Comment);
            builder.HasOne(y => y.User).WithMany(x => x.Comments).HasForeignKey(y => y.UserId); 
            builder.HasOne(c => c.lecture).WithMany(l => l.Comments).HasForeignKey(c => c.LectureId).OnDelete(DeleteBehavior.Cascade);    
            //indexes
        }
    }
}
