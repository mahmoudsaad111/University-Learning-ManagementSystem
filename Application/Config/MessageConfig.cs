using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Config
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(m => m.MessageContent).IsRequired(true).HasMaxLength(500);


            // Primary Key 
            builder.HasKey(m => m.MessageId);

            // Relations
            builder.HasOne(m=>m.User).WithMany(u=>u.Messages).HasForeignKey(m=>m.UserId);
            builder.HasOne(m => m.Group).WithMany(g => g.Messages).HasForeignKey(m => m.GroupId);

            // Index
            builder.HasIndex(m => m.GroupId).IsUnique(true);
        }
    }
}
