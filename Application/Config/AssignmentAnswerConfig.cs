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

	public class AssignmentAnswerConfig : IEntityTypeConfiguration<AssignmentAnswer>
    {
        public void Configure(EntityTypeBuilder<AssignmentAnswer> builder)
        {
            builder.ToTable("AssignmentAnswers");
            // properties
            builder.Property(x=>x.Mark).IsRequired(true);
            builder.Property(x => x.Description).IsRequired(false);
           
            // the nect two lines are commented because it's not required and i will update the database
            //&builder.Property(x => x.Url).IsRequired(true); 
            //&builder.Property(x=>x.CreatedBy).IsRequired(true);
            // keys
            builder.HasKey(x => x.AssignmentAnswer_id);
            // Relations
            builder.HasOne(x => x.Assignment).WithMany(y => y.AssignmentAnswers).HasForeignKey(x => x.AssignmentId);
            builder.HasOne(x => x.Student).WithMany(y => y.AssignmentAnswers).HasForeignKey(x => x.StudentId);

            //
            builder.HasMany(x => x.AssignmentAnswerResources).WithOne(y => y.AssignmentAnswer).HasForeignKey(y => y.AssignmentAnswerId).OnDelete(DeleteBehavior.NoAction);
            // Indexes
            builder.HasIndex(x => new { x.StudentId, x.AssignmentId }).IsUnique(true);
        }
    }
}
