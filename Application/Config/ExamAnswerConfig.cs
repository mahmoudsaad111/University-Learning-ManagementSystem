using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Application.Config
{
    public class ExamAnswerConfig : IEntityTypeConfiguration<ExamAnswer>

    {
        public void Configure(EntityTypeBuilder<ExamAnswer> builder)
        {
            builder.ToTable("ExamAnswers");
            // properties 
            builder.Property(x=>x.Url).IsRequired(true);
            builder.Property(x => x.DescriptionOrNote).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Mark).IsRequired(true);

            //keys
            builder.HasKey(x => x.ExamAnswerId);
            // relationships
            
            // this relation in examConfig
            //builder.HasOne(x => x.Exam).WithMany(y => y.ExamAnswers).HasForeignKey(x => x.ExamId);
            builder.HasOne(x => x.Student).WithMany(y => y.ExamAnswers).HasForeignKey(x => x.StudentId);
            //indexes
            builder.HasIndex(x => new { x.ExamId, x.StudentId }).IsUnique(true); 

        }
    }
}
